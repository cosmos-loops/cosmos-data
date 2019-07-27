using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.Store
{
    public interface IStoreContext
    {
        void Commit();

        void Commit(Action callback);

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task CommitAsync(Action callback, CancellationToken cancellationToken = default);
    }
}