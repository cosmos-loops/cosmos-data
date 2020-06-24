using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;
using Cosmos.Validations.Parameters;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of unsafe writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUnsafeWriteableStore<in TEntity> where TEntity : class, IEntity, new()
    {
        #region Unsafe remove

        /// <summary>
        /// Unsafe remove by entity
        /// </summary>
        /// <param name="entity"></param>
        void UnsafeRemove([NotNull] TEntity entity);

        /// <summary>
        /// Unsafe remove by a set of entity
        /// </summary>
        /// <param name="entities"></param>
        void UnsafeRemove([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Unsafe remove by entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unsafe remove by a set of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion
    }

    /// <summary>
    /// Interface of unsafe writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IUnsafeWriteableStore<in TEntity, in TKey> : IUnsafeWriteableStore<TEntity> where TEntity : class, IEntity<TKey>, new()
    {
        #region Unsafe remove

        /// <summary>
        /// Unsafe remove by id
        /// </summary>
        /// <param name="id"></param>
        void UnsafeRemove(TKey id);

        /// <summary>
        /// Unsafe remove by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion
    }
}