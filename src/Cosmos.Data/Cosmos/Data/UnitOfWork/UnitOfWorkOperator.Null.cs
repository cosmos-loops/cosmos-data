using System.Threading;
using System.Threading.Tasks;
using Cosmos.Disposables;

namespace Cosmos.Data.UnitOfWork
{
    /// <summary>
    /// Null UnitOfWork Operator
    /// </summary>
    public sealed class NullUnitOfWorkOperator : DisposableObjects, IUnitOfWorkOperator
    {
        /// <summary>
        /// Gets an instance of <see cref="NullUnitOfWorkOperator"/>
        /// </summary>
        public static NullUnitOfWorkOperator Instance { get; } = new NullUnitOfWorkOperator();

        private NullUnitOfWorkOperator() { }

        /// <summary>
        /// Commit.
        /// </summary>
        public void Commit() { }

        /// <summary>
        /// Commit async.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}