using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Models;
using Cosmos.Validation.Annotations;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IWriteableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Add

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entities"></param>
        void Add([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        void Update([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] TEntity entity);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] IEnumerable<TEntity> entities);

        #endregion

        #region Remove

        /// <summary>
        /// Remove by entity
        /// </summary>
        /// <param name="entity"></param>
        void Remove([NotNull] TEntity entity);

        /// <summary>
        /// Remove by a set of entity
        /// </summary>
        /// <param name="entities"></param>
        void Remove([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Remove by entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove by a set of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion
    }

    /// <summary>
    /// Interface of writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IWriteableStore<TEntity, in TKey> : IWriteableStore<TEntity> where TEntity : class, IEntity<TKey>, new()
    {
        #region Remove

        /// <summary>
        /// Remove by id
        /// </summary>
        /// <param name="id"></param>
        void Remove(TKey id);

        /// <summary>
        /// Remove by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion
    }
}