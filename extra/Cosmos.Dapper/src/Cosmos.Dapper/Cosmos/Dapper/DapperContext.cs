using System;
using System.Collections.Concurrent;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AspectCore.Extensions.Reflection;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Dapper.Operations;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;
using Dapper;

namespace Cosmos.Dapper
{
    public abstract class DapperContext<TContext, TConnection> : IDapperContext, IWithConnection<TConnection>, IWithSQLGenerator
        where TContext : DapperContext<TContext, TConnection>, IDapperContext, IWithConnection<TConnection>, IWithSQLGenerator
        where TConnection : class, IDbConnection
    {
        private readonly IDapperConnector<TConnection> _connector;
        private readonly DapperConfig _mappingConfig;
        private readonly IDapperContextParams _contextParams;
        
        protected DapperContext(TConnection connection, IDapperContextParams @params)
        {
            var eap = LazyThreadSafetyMode.ExecutionAndPublication;
            ((IWithSQLGenerator) this).SqlGenerator = @params.GetSqlGenerator();
            _contextParams = @params;
            _mappingConfig = DapperConfigAccessor.Cache(connection.ConnectionString);
            _connector = new DapperConnector<TConnection>(connection, _mappingConfig, ((IWithSQLGenerator)this).SqlGenerator);
            _lazyQueryOperators = new Lazy<IDapperQueryOperator>(() => new DapperQueryOperator(_connector, _mappingConfig, InjectTransaction), eap);
            _lazyCommandOperators = new Lazy<IDapperCommandOperator>(() => new DapperCommandOperator(_connector, _mappingConfig, InjectTransaction), eap);
            _lazyBulkInsertOperators = new Lazy<IDapperBulkInsertOperator>(() => @params.GetBulkInsertOperator(_connector), eap);
            _dapperSetCache = new ConcurrentDictionary<int, object>();

            OriginConnectionString = connection.ConnectionString;
           
            OnContextCreatingScoped();
        }

        #region TSQLGenerator

        ISQLGenerator IWithSQLGenerator.SqlGenerator { get; set; }

        #endregion

        #region Connection String

        public string OriginConnectionString { get; }

        #endregion

        #region transaction

        public IDbTransaction Transaction => _connector.TransactionWrapper.CurrentTransaction;

        public void Commit()
        {
            Commit(null);
        }

        public void Commit(Action callback)
        {
            try
            {
                _connector.TransactionWrapper.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                callback?.Invoke();
            }
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run(() => Commit(null), cancellationToken);
        }

        public Task CommitAsync(Action callback, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => Commit(callback), cancellationToken);
        }

        public void Rollback()
        {
            _connector.TransactionWrapper.Rollback();
        }

        private CommandDefinition InjectTransaction(CommandDefinition commandDefinition)
        {
            if (Transaction == null)
            {
                return commandDefinition;
            }

            var command = new CommandDefinition(
                commandDefinition.CommandText,
                commandDefinition.Parameters,
                Transaction,
                commandDefinition.CommandTimeout,
                commandDefinition.CommandType,
                commandDefinition.Flags,
                commandDefinition.CancellationToken);

            return command;
        }

        #endregion

        #region operation proxy

        private readonly Lazy<IDapperQueryOperator> _lazyQueryOperators;
        private readonly Lazy<IDapperCommandOperator> _lazyCommandOperators;
        private readonly Lazy<IDapperBulkInsertOperator> _lazyBulkInsertOperators;

        public IDapperQueryOperator QueryOperators => _lazyQueryOperators.Value;

        public IDapperCommandOperator CommandOperators => _lazyCommandOperators.Value;

        public IDapperEntityOperator EntityOperators => _connector;

        public IDapperBulkInsertOperator BulkInsertOperators => _lazyBulkInsertOperators.Value;

        #endregion

        #region dapper set entry

        private readonly ConcurrentDictionary<int, object> _dapperSetCache;

        public Lazy<IDapperSet<TEntity>> DapperSetLazy<TEntity>(string bindingPropertyName) where TEntity : class, IEntity, new()
        {
            var hash = bindingPropertyName.GetHashCode();
            return _dapperSetCache.GetOrAdd(hash, DapperSet.LazyEntity<TContext, TEntity, TConnection>(this as TContext, bindingPropertyName)) as Lazy<IDapperSet<TEntity>>;
        }

        #endregion

        #region action entry

        public ISQLActionEntry GetActionEntry(ISQLPredicate[] dataFilterPredicates = null)
            => _connector.GetActionEntry(_contextParams, dataFilterPredicates);

        public ISQLActionEntry<TEntity> GetActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new()
            => _connector.GetActionEntry<TEntity>(_contextParams, dataFilterPredicates);

        public ISQLActionAsyncEntry GetAsynchronousActionEntry(ISQLPredicate[] dataFilterPredicates = null)
            => _connector.GetAsynchronousActionEntry(_contextParams, dataFilterPredicates);

        public ISQLActionAsyncEntry<TEntity> GetAsynchronousActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new()
            => _connector.GetAsynchronousActionEntry<TEntity>(_contextParams, dataFilterPredicates);

        #endregion

        #region sqlkata entry

        public QueryBuilder GetSqlKataQueryBuilder() => new QueryBuilder(_connector, _mappingConfig.GetCompiler(), _mappingConfig.Options, false);

        public EntityQueryBuilder GetSqlKataEntityQueryBuilder() => new EntityQueryBuilder(_connector, _mappingConfig.GetCompiler(), _mappingConfig.Options, false);

        public MultipleQueryBuilder GetSqlKataMultipleQueryBuilder() => new MultipleQueryBuilder(_connector, _mappingConfig.GetCompiler());

        public Func<QueryBuilder> SqlKataQueryBuilderFunc()
        {
            return GetSqlKataQueryBuilder;
        }

        #endregion

        #region on context creating

        internal void OnContextCreatingScoped()
        {
            var method = typeof(DapperSet).GetMethod(nameof(DapperSet.LazyEntity), BindingFlags.Public | BindingFlags.Static);

            if (method == null)
                throw new InvalidOperationException($"Cannot call {nameof(DapperSet.LazyEntity)} method.");

            var typeOfContext = typeof(TContext);
            var typeOfConnection = typeof(TConnection);
            var allPropertyTuples = DapperContextualManager.GetAllProperties<TContext, TConnection>();

            foreach (var tuple in allPropertyTuples)
            {
                var currentMethod = method.MakeGenericMethod(typeOfContext, tuple.entityType, typeOfConnection);
                var currentMethodReflector = currentMethod.GetReflector();
                var invokedResult = currentMethodReflector.Invoke(null, this, tuple.name);
                tuple.property.GetReflector().SetValue(this, invokedResult);
            }
        }

        #endregion

        #region on model creating

        protected virtual void OnModelCreating(DapperClassBuilder modelBuilder)
        {
            using (var scanner = new EntityMapScanner<TContext>(EntityMapType))
            {
                foreach (var map in scanner.ScanAndReturnInstances())
                {
                    map?.Map(modelBuilder);
                }
            }
        }

        protected abstract Type EntityMapType { get; }

        #endregion

        #region internal handler

        IDapperConnector<TConnection> IWithConnection<TConnection>.Connector => _connector;

        internal DapperConfig MappingConfig => _mappingConfig;

        #endregion

        #region dispose

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _connector.Dispose();
            }

            _disposed = true;
        }

        #endregion

    }
}