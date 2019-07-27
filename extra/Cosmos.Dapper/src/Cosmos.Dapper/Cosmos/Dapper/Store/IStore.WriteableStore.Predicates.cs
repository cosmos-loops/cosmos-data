using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public interface IPredicateWriteableStore<TEntity> where TEntity : class, IEntity, new()
    {
        bool RemoveById(dynamic id);
        Task<bool> RemoveByIdAsync(dynamic id, CancellationToken cancellationToken = default);
        bool Remove(object predicate);
        Task<bool> RemoveAsync(object predicate, CancellationToken cancellationToken = default);
    }
}