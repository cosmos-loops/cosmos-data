using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Store;
using Cosmos.Disposables;

namespace Cosmos.Data.UnitOfWork
{
    public class UnitOfWorkOperator : DisposableObjects, IUnitOfWorkOperator
    {
        public UnitOfWorkOperator(IStoreContextManager manager, bool manualCommit = false, bool isAsynchronous = false)
        {
            manager.CheckNull(nameof(manager));
            UnitOfWorkManager = manager;
            AddDisposableAction("_unitOfWorkDisposeAction", DefaultDisposeAction);
            Commited = false;
            ManualCommit = manualCommit;
            IsAsynchronous = isAsynchronous;
        }

        public UnitOfWorkOperator(IStoreContextManager manager, Action disposeAction, bool manualCommit = false, bool isAsynchronous = false)
        {
            manager.CheckNull(nameof(manager));
            UnitOfWorkManager = manager;
            AddDisposableAction("_unitOfWorkDisposeAction", disposeAction);
            Commited = false;
            ManualCommit = manualCommit;
            IsAsynchronous = isAsynchronous;
        }

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

        public void Commit()
        {
            if (!Commited)
            {
                UnitOfWorkManager.Commit();
                Commited = true;
            }
        }

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