using System;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of repository
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Scoped initialize
        /// </summary>
        /// <param name="manager"></param>
        [RepositoryInitialize]
        void ScopedInitialize(IStoreContextManager manager);
    }
}