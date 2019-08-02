using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;
using Cosmos.Validations.Parameters;
using DotNetCore.Collections.Paginable;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of Queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IQueryableStore<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
    {
        /// <summary>
        /// Counts by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Counts by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Exists or not by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exist(TKey id);

        /// <summary>
        /// Exists or not by a set of id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Exist(TKey[] ids);

        /// <summary>
        /// Exists or not by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Exists or not by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(TKey id);

        /// <summary>
        /// Exists or not by a set of id async
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(TKey[] ids);

        /// <summary>
        /// Exists or not by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistAsync([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find result by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(TKey id);

        /// <summary>
        /// Find a collection of result by a set of id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TEntity> FindByIds(params TKey[] ids);

        /// <summary>
        /// Find a collection of result by a set of id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TEntity> FindByIds(IEnumerable<TKey> ids);

        /// <summary>
        /// Find a collection of result by by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find a collection of paged result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);

        /// <summary>
        /// Find first or default result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindFirstOrDefault([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Query pageable results by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPage<TEntity> QueryPage([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);

        /// <summary>
        /// Find result by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a collection of result by a set of id async
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindByIdsAsync(params TKey[] ids);

        /// <summary>
        /// Find a collection of result by a set of id async
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a collection of paged results by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find first or default result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstOrDefaultAsync([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Query pageable results by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IPage<TEntity>> QueryPageAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
    }
}