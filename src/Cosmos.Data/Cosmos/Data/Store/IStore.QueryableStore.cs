using System;
using System.Collections.Generic;
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
    public interface IQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Count

        /// <summary>
        /// Counts by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Counts by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Exists

        /// <summary>
        /// Exists or not by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exist([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Exists or not by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find

        /// <summary>
        /// Find a collection of paged result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find a collection of paged results by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find First

        /// <summary>
        /// Find first  result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindFirst([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find first  result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find First Or Default

        /// <summary>
        /// Find first or default result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindFirstOrDefault([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find first or default result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstOrDefaultAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find Single

        /// <summary>
        /// Find single result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindSingle([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find single result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find Single or Default

        /// <summary>
        /// Find single or default result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindSingleOrDefault([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find single or default result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleOrDefaultAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Get One

        /// <summary>
        /// Get one or null...
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity GetOne([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get one or null...
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Get page

        /// <summary>
        /// Query pageable results by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPage<TEntity> GetPage([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);

        /// <summary>
        /// Query pageable results by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IPage<TEntity>> GetPageAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        #endregion
    }

    /// <summary>
    /// Interface of Queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IQueryableStore<TEntity, in TKey> : IQueryableStore<TEntity> where TEntity : class, IEntity<TKey>, new()
    {
        #region Find by id

        /// <summary>
        /// Find result by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(TKey id);

        /// <summary>
        /// Find result by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion

        #region Get One

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetOne([NotNull] TKey id);

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync([NotNull] TKey id, CancellationToken cancellationToken = default);

        #endregion
    }
}