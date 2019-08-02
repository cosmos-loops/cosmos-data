using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.UnitOfWork
{
    /// <summary>
    /// Interface of UnitOfWork operator
    /// </summary>
    public interface IUnitOfWorkOperator : IDisposable
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
    }
}