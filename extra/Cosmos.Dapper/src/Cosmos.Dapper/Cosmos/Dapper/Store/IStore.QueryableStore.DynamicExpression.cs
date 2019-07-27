using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public interface IDynamicExpressionQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        TEntity FindFirst(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);
        TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);
        TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);
        TEntity FindSingleOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);
        Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);
        Task<IEnumerable<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true);
        Task<IEnumerable<TEntity>> FindByPredicate(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> QueryPage(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true);
        Task<IEnumerable<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}