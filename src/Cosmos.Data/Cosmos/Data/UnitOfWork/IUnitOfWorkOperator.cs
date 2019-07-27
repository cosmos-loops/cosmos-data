using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Data.UnitOfWork
{
    public interface IUnitOfWorkOperator : IDisposable
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}