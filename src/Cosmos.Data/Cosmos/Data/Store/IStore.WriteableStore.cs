using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;
using Cosmos.Validations.Parameters;

namespace Cosmos.Data.Store
{
    public interface IWriteableStore<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
    {
        void Add([NotNull] TEntity entity);
        void Add([NotNull] IEnumerable<TEntity> entities);
        Task AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);
        Task AddAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Update([NotNull] TEntity entity);
        void Update([NotNull] IEnumerable<TEntity> entities);
        Task UpdateAsync([NotNull] TEntity entity);
        Task UpdateAsync([NotNull] IEnumerable<TEntity> entities);
        void Remove(TKey id);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TKey> ids);
        void Remove(IEnumerable<TEntity> entities);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TKey id, CancellationToken cancellationToken = default);
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
        Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}