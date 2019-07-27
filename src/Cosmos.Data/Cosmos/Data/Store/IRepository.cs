using System;

namespace Cosmos.Data.Store
{
    public interface IRepository : IDisposable
    {
        [RepositoryInitialize]
        void ScopedInitialize(IStoreContextManager manager);
    }
}