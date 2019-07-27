using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Context;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper
{
    public interface IDapperSet<TEntity> : IDapperSet, IDbSet<TEntity> where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Delete(ISQLPredicate predicate);
        Task DeleteAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default);
        TEntity FindById(dynamic id);
        Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default);
        TEntity FindSingleBySql(string sql);
        Task<TEntity> FindSingleBySqlAsync(string sql, CancellationToken cancellationToken = default);
        TEntity FindSingleOrDefaultBySql(string sql);
        Task<TEntity> FindSingleOrDefaultBySqlAsync(string sql, CancellationToken cancellationToken = default);
        TEntity FindFirstBySql(string sql);
        Task<TEntity> FindFirstBySqlAsync(string sql, CancellationToken cancellationToken = default);
        TEntity FindFirstOrDefaultBySql(string sql);
        Task<TEntity> FindFirstOrDefaultBySqlAsync(string sql, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> FindListBySql(string sql);
        Task<IEnumerable<TEntity>> FindListBySqlAsync(string sql, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> FindListByPredicate(ISQLPredicate predicate, SQLSortSet sort);
        Task<IEnumerable<TEntity>> FindListByPredicateAsync(ISQLPredicate predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        TEntity FindFirstBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindFirstBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        TEntity FindFirstOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindFirstOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        TEntity FindSingleBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindSingleBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        TEntity FindSingleOrDefaultBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<TEntity> FindSingleOrDefaultBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        IEnumerable<TEntity> FindListBySqlKata(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        Task<IEnumerable<TEntity>> FindListBySqlKataAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
        int Count(ISQLPredicate predicate);
        Task<int> CountAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default);
    }
}