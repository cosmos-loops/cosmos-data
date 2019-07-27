using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;
using Cosmos.Validations.Parameters;

namespace Cosmos.Data.Store
{
    public interface IUnsafeWriteableStore<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
    {
        void UnsafeRemove(TKey id);
        void UnsafeRemove([NotNull] TEntity entity);
        void UnsafeRemove(IEnumerable<TKey> ids);
        void UnsafeRemove([NotNull] IEnumerable<TEntity> entities);
        Task UnsafeRemoveAsync(TKey id, CancellationToken cancellationToken = default);
        Task UnsafeRemoveAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);
        Task UnsafeRemoveAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
        Task UnsafeRemoveAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}