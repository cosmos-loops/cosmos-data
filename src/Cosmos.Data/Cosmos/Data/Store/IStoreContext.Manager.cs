using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of Context Manager
    /// </summary>
    public interface IStoreContextManager
    {
        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        void Register(Type type, IStoreContext context);
    }
}