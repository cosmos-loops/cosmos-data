using System;
using System.Data;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Operations;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Data.Store;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper
{
    public interface IDapperContext : IStoreContext, IDisposable
    {
        string OriginConnectionString { get; }

        IDbTransaction Transaction { get; }

        void Rollback();

        IDapperQueryOperator QueryOperators { get; }

        IDapperCommandOperator CommandOperators { get; }

        IDapperEntityOperator EntityOperators { get; }

        IDapperBulkInsertOperator BulkInsertOperators { get; }

        ISQLActionEntry GetActionEntry(ISQLPredicate[] dataFilterPredicates = null);

        ISQLActionEntry<TEntity> GetActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new();

        ISQLActionAsyncEntry GetAsynchronousActionEntry(ISQLPredicate[] dataFilterPredicates = null);

        ISQLActionAsyncEntry<TEntity> GetAsynchronousActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new();

        QueryBuilder GetSqlKataQueryBuilder();

        EntityQueryBuilder GetSqlKataEntityQueryBuilder();

        MultipleQueryBuilder GetSqlKataMultipleQueryBuilder();
        
        Func<QueryBuilder> SqlKataQueryBuilderFunc();

        Lazy<IDapperSet<TEntity>> DapperSetLazy<TEntity>(string bindingPropertyName) where TEntity : class, IEntity, new();
    }
}