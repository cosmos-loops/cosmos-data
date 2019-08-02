using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;
using Cosmos.Validations.Parameters;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IWriteableStore<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
    {
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

        /// <summary>
        /// Remove by id
        /// </summary>
        /// <param name="id"></param>
        void Remove(TKey id);

        /// <summary>
        /// Remove by entity
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove by a set of id
        /// </summary>
        /// <param name="ids"></param>
        void Remove(IEnumerable<TKey> ids);

        /// <summary>
        /// Remove by a set of entity
        /// </summary>
        /// <param name="entities"></param>
        void Remove(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Remove by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove by entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove by a set of id async
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove by a set of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}