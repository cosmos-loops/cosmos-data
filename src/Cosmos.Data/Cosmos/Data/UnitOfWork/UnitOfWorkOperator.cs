using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Store;
using Cosmos.Disposables;

namespace Cosmos.Data.UnitOfWork
{
    /// <summary>
    /// UnitOfWork operator
    /// </summary>
    public class UnitOfWorkOperator : DisposableObjects, IUnitOfWorkOperator
    {
        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkOperator"/>
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="manualCommit"></param>
        /// <param name="isAsynchronous"></param>
        public UnitOfWorkOperator(IStoreContextManager manager, bool manualCommit = false, bool isAsynchronous = false)
        {
            manager.CheckNull(nameof(manager));
            UnitOfWorkManager = manager;
            AddDisposableAction("_unitOfWorkDisposeAction", DefaultDisposeAction);
            Commited = false;
            ManualCommit = manualCommit;
            IsAsynchronous = isAsynchronous;
        }

        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkOperator"/>
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="disposeAction"></param>
        /// <param name="manualCommit"></param>
        /// <param name="isAsynchronous"></param>
        public UnitOfWorkOperator(IStoreContextManager manager, Action disposeAction, bool manualCommit = false, bool isAsynchronous = false)
        {
            manager.CheckNull(nameof(manager));
            UnitOfWorkManager = manager;
            AddDisposableAction("_unitOfWorkDisposeAction", disposeAction);
            Commited = false;
            ManualCommit = manualCommit;
            IsAsynchronous = isAsynchronous;
        }

        /// <summary>
        /// Gets UnitOfWork Manager
        /// </summary>
        protected IStoreContextManager UnitOfWorkManager { get; }

        private void DefaultDisposeAction()
        {
            CommitWhenDisposing();
            Commited = true;
        }

        private bool Commited { get; set; }

        private bool ManualCommit { get; set; }

        private bool IsAsynchronous { get; set; }

        private bool NeedToCommitWhenDispose()
        {
            return !Commited && !ManualCommit;
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            if (!Commited)
            {
                UnitOfWorkManager.Commit();
                Commited = true;
            }
        }

        /// <summary>
        /// Commit async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (!Commited)
            {
                await UnitOfWorkManager.CommitAsync(cancellationToken);
                Commited = true;
            }
        }

        private void CommitWhenDisposing()
        {
            if (NeedToCommitWhenDispose())
            {
                if (IsAsynchronous)
                {
                    CommitAsync().GetAwaiter().GetResult();
                }
                else
                {
                    Commit();
                }
            }
        }
    }
}