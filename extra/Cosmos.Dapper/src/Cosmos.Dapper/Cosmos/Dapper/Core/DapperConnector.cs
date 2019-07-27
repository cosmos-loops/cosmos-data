using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Actions;
using Cosmos.Data.Statements;
using Cosmos.Data.Transaction;
using Cosmos.Domain.Core;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperConnector : IDapperConnector
    {
        private readonly IDapperMappingConfig _config;
        private readonly IDapperImplementor _proxy;
        private readonly ITransactionWrapper _transactionWrapper;
        private IDbTransaction Transaction => _transactionWrapper.CurrentTransaction;

        public DapperConnector(IDbConnection connection, IDapperMappingConfig config, ISQLGenerator sqlGenerator)
        {
            _proxy = new DapperImplementor(config, sqlGenerator);

            _config = config;

            Connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _transactionWrapper = new TransactionWrapper(Connection);

            TryOpenConnection();
        }

        #region Connection

        public IDbConnection Connection { get; }

        private bool TryOpenConnection()
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Transaction

        public bool HasActiveTransaction => Transaction != null;

        public void BeginTransaction(IsolationLevel il = IsolationLevel.ReadCommitted)
        {
            _transactionWrapper.Begin(il);
        }

        public ITransactionWrapper TransactionWrapper => _transactionWrapper;

        public void Commit()
        {
            _transactionWrapper.Commit();
        }

        public void Rollback()
        {
            _transactionWrapper.Rollback();
        }

        #endregion

        #region Simple transaction action

        public void RunInTransaction(Action action)
        {
            BeginTransaction();
            try
            {
                action();
                Commit();
            }
            catch (Exception)
            {
                if (HasActiveTransaction)
                    Rollback();
                throw;
            }
        }

        public T RunInTransaction<T>(Func<T> func)
        {
            BeginTransaction();
            try
            {
                T result = func();
                Commit();
                return result;
            }
            catch (Exception)
            {
                if (HasActiveTransaction)
                    Rollback();
                throw;
            }
        }

        #endregion

        #region Insert

        public void Insert<T>(IEnumerable<T> entities, IDbTransaction transaction) where T : class
            => _proxy.Insert(Connection, entities, transaction);

        public Task InsertAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entities, transaction, cancellationToken);

        public void Insert<T>(IEnumerable<T> entities) where T : class
            => _proxy.Insert(Connection, entities, Transaction);

        public Task InsertAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entities, Transaction, cancellationToken);

        public dynamic Insert<T>(T entity, IDbTransaction transaction) where T : class
            => _proxy.Insert(Connection, entity, transaction);

        public Task<dynamic> InsertAsync<T>(T entity, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entity, transaction, cancellationToken);

        public dynamic Insert<T>(T entity) where T : class
            => _proxy.Insert(Connection, entity, Transaction);

        public Task<dynamic> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entity, Transaction, cancellationToken);

        #endregion

        #region Update

        public bool Update<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entity, transaction, filters, ignoreAllKeyProperties);

        public Task<bool> UpdateAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.UpdateAsync(Connection, entity, transaction, filters, ignoreAllKeyProperties, cancellationToken);

        public bool Update<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entity, Transaction, filters, ignoreAllKeyProperties);

        public Task<bool> UpdateAsync<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.UpdateAsync(Connection, entity, Transaction, filters, ignoreAllKeyProperties, cancellationToken);

        public bool Update<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entities, transaction, filters, ignoreAllKeyProperties);

        public Task<bool> UpdateAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.UpdateAsync(Connection, entities, transaction, filters, ignoreAllKeyProperties, cancellationToken);

        public bool Update<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entities, Transaction, filters, ignoreAllKeyProperties);

        public Task<bool> UpdateAsync<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.UpdateAsync(Connection, entities, Transaction, filters, ignoreAllKeyProperties, cancellationToken);

        #endregion

        #region Delete/Remove

        public bool Delete<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entity, transaction, filters);

        public Task<bool> DeleteAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.DeleteAsync(Connection, entity, transaction, filters, cancellationToken);

        public bool Delete<T>(T entity, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entity, Transaction, filters);

        public Task<bool> DeleteAsync<T>(T entity, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, entity, Transaction, filters, cancellationToken);

        public bool Delete<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entities, transaction, filters);

        public Task<bool> DeleteAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, entities, transaction, filters, cancellationToken);

        public bool Delete<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entities, Transaction, filters);

        public Task<bool> DeleteAsync<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, entities, Transaction, filters, cancellationToken);

        #endregion

        #region Get

        public T Get<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Get<T>(Connection, id, transaction, filters);

        public Task<T> GetAsync<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetAsync<T>(Connection, id, transaction, filters, cancellationToken);

        public T Get<T>(dynamic id, ISQLPredicate[] filters = null) where T : class
            => _proxy.Get<T>(Connection, id, Transaction, filters);

        public Task<T> GetAsync<T>(dynamic id, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetAsync<T>(Connection, id, Transaction, filters, cancellationToken);

        #endregion

        #region GetMultiple

        public IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate, IDbTransaction transaction)
            => _proxy.GetMultiple(Connection, predicate, transaction);

        public Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, IDbTransaction transaction, CancellationToken cancellationToken = default)
            => _proxy.GetMultipleAsync(Connection, predicate, transaction, cancellationToken);

        public IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate)
            => _proxy.GetMultiple(Connection, predicate, Transaction);

        public Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, CancellationToken cancellationToken = default)
            => _proxy.GetMultipleAsync(Connection, predicate, Transaction, cancellationToken);

        #endregion

        public void ClearCache() => _proxy.SQLGenerator.Configuration.ClearCache();

        public Guid GetNextGuid() => _proxy.SQLGenerator.Configuration.GetNextGuid();

        public IClassMap GetMap<T>() where T : class => _proxy.SQLGenerator.Configuration.GetMap<T>();

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                if (HasActiveTransaction)
                    Transaction.Rollback();
                Connection.Close();
            }
        }

        #region Actions

        public ISQLActionEntry GetActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters)
        {
            return new SQLActionSyncEntry(new SQLActionSet(this, _config), contextParams, filters);
        }

        public ISQLActionEntry<T> GetActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new()
        {
            return new SQLActionSyncEntry<T>(new SQLActionSet<T>(this, _config), contextParams, filters);
        }

        public ISQLActionAsyncEntry GetAsynchronousActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters)
        {
            return new SQLActionAsyncEntry(new SQLActionSet(this, _config), contextParams, filters);
        }

        public ISQLActionAsyncEntry<T> GetAsynchronousActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new()
        {
            return new SQLActionAsyncEntry<T>(new SQLActionSet<T>(this, _config), contextParams, filters);
        }

        #endregion
    }
}