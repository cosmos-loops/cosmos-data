using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.Store
{
    public interface IStoreContextManager
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
        void Register(Type type, IStoreContext context);
    }
}