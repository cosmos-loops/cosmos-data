using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public interface IPredicateQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        int Count(object predicate);
        Task<int> CountAsync(object predicate, CancellationToken cancellationToken = default);
        bool Exist(object predicate);
        Task<bool> ExistAsync(object predicate, CancellationToken cancellationToken = default);
        TEntity FindFirst(object predicate, SQLSortSet sort, bool buffered = true);
        TEntity FindFirstOrDefault(object predicate, SQLSortSet sort, bool buffered = true);
        TEntity FindSingle(object predicate, SQLSortSet sort, bool buffered = true);
        TEntity FindSingleOrDefault(object predicate, SQLSortSet sort, bool buffered = true);
        Task<TEntity> FindFirstAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        Task<TEntity> FindFirstOrDefaultAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        Task<TEntity> FindSingleAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        Task<TEntity> FindSingleOrDefaultAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> FindByPredicate(object predicate, SQLSortSet sort, bool buffered = true);
        Task<IEnumerable<TEntity>> FindByPredicateAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> FindByPredicate(object predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true);
        Task<IEnumerable<TEntity>> FindByPredicateAsync(object predicate, SQLSortSet sort, int limitFrom, int limitTo, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> QueryPage(object predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true);
        Task<IEnumerable<TEntity>> QueryPageAsync(object predicate, SQLSortSet sort, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}