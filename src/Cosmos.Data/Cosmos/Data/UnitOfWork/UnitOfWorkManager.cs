using System;
using Cosmos.Data.Store;

namespace Cosmos.Data.UnitOfWork
{
    public class UnitOfWorkManager
    {
        public UnitOfWorkManager(IStoreContextManager manager)
        {
            ScopedUnitOfWorkManager = manager;
        }

        private IStoreContextManager ScopedUnitOfWorkManager { get; }

        public IStoreContextManager GetScopedUnitOfWorkManager() => ScopedUnitOfWorkManager;

        private bool IsScopedNull() => ScopedUnitOfWorkManager == null;

        public IUnitOfWorkOperator CreateUnitOfWork()
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork();
        }

        public IUnitOfWorkOperator CreateUnitOfWork(bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork(manualCommit);
        }

        public IUnitOfWorkOperator CreateUnitOfWork(Action disposedAction)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork(disposedAction);
        }

        public IUnitOfWorkOperator CreateUnitOfWork(Action disposedAction, bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork(disposedAction, manualCommit);
        }

        public IUnitOfWorkOperator CreateUnitOfWorkAsync()
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync();
        }

        public IUnitOfWorkOperator CreateUnitOfWorkAsync(bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync(manualCommit);
        }

        public IUnitOfWorkOperator CreateUnitOfWorkAsync(Action disposedAction)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync(disposedAction);
        }

        public IUnitOfWorkOperator CreateUnitOfWorkAsync(Action disposedAction, bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync(disposedAction, manualCommit);
        }
    }
}