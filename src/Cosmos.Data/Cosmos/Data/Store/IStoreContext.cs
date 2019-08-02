using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Interface of Store Context
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit
        /// </summary>
        /// <param name="callback"></param>
        void Commit(Action callback);

        /// <summary>
        /// Commit async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit async
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CommitAsync(Action callback, CancellationToken cancellationToken = default);
    }
}