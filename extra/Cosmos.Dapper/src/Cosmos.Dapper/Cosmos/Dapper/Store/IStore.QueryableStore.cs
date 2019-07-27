using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Actions;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public interface IQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        bool ExistById(dynamic id);
        Task<bool> ExistByIdAsync(dynamic id);
        TEntity FindById(dynamic id);
        Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default);
        ISQLActionEntry ActionEntry { get; }
        ISQLActionEntry<TEntity> EntityEntry { get; }
        ISQLActionAsyncEntry AsynchronousActionEntry { get; }
        ISQLActionAsyncEntry<TEntity> AsynchronousEntityEntry { get; }
    }

    public interface IQueryableStore<TEntity, in TKey> : IQueryableStore<TEntity>
        where TEntity : class, IEntity<TKey>, new()
    {
        TEntity FindById(TKey id);
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
        bool ExistById(TKey id);
        Task<bool> ExistByIdAsync(TKey id, CancellationToken cancellationToken = default);
    }
}