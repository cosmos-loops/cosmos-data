using System.Threading;
using System.Threading.Tasks;
using Cosmos.Disposables;

namespace Cosmos.Data.UnitOfWork
{
    public sealed class NullUnitOfWorkOperator : DisposableObjects, IUnitOfWorkOperator
    {
        public static NullUnitOfWorkOperator Instance { get; } = new NullUnitOfWorkOperator();

        private NullUnitOfWorkOperator() { }

        public void Commit() { }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}