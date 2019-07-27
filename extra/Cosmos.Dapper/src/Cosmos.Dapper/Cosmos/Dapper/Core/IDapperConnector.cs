using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;
using Cosmos.Data.Transaction;
using Cosmos.Domain.Core;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Dapper.Core
{
    public interface IDapperConnector : IDapperEntityOperator, IDisposable
    {
        bool HasActiveTransaction { get; }
        IDbConnection Connection { get; }
        void BeginTransaction(IsolationLevel il = IsolationLevel.ReadCommitted);
        ITransactionWrapper TransactionWrapper { get; }
        void Commit();
        void Rollback();
        void RunInTransaction(Action action);
        IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate, IDbTransaction transaction);
        Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, IDbTransaction transaction, CancellationToken cancellationToken = default);
        IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate);
        Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, CancellationToken cancellationToken = default);
        void ClearCache();
        Guid GetNextGuid();
        IClassMap GetMap<T>() where T : class;
        ISQLActionEntry GetActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters);
        ISQLActionEntry<T> GetActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new();
        ISQLActionAsyncEntry GetAsynchronousActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters);
        ISQLActionAsyncEntry<T> GetAsynchronousActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new();
    }
}