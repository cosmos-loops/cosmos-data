using System;
using Cosmos.Data.Store;

namespace Cosmos.Data.UnitOfWork
{
    /// <summary>
    /// UnitOfWork Manager
    /// </summary>
    public class UnitOfWorkManager
    {
        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkManager"/>
        /// </summary>
        /// <param name="manager"></param>
        public UnitOfWorkManager(IStoreContextManager manager)
        {
            ScopedUnitOfWorkManager = manager;
        }

        private IStoreContextManager ScopedUnitOfWorkManager { get; }

        /// <summary>
        /// Get scoped unit of work manager
        /// </summary>
        /// <returns></returns>
        public IStoreContextManager GetScopedUnitOfWorkManager() => ScopedUnitOfWorkManager;

        private bool IsScopedNull() => ScopedUnitOfWorkManager == null;

        /// <summary>
        /// Create unit of work
        /// </summary>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWork()
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork();
        }

        /// <summary>
        /// Create unit of work
        /// </summary>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWork(bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork(manualCommit);
        }

        /// <summary>
        /// Create unit of work
        /// </summary>
        /// <param name="disposedAction"></param>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWork(Action disposedAction)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork(disposedAction);
        }

        /// <summary>
        /// Create unit of work
        /// </summary>
        /// <param name="disposedAction"></param>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWork(Action disposedAction, bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWork(disposedAction, manualCommit);
        }

        /// <summary>
        /// Create unit of work async
        /// </summary>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWorkAsync()
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync();
        }

        /// <summary>
        /// Create unit of work async
        /// </summary>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWorkAsync(bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync(manualCommit);
        }

        /// <summary>
        /// Create unit of work async
        /// </summary>
        /// <param name="disposedAction"></param>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWorkAsync(Action disposedAction)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync(disposedAction);
        }

        /// <summary>
        /// Create unit of work async
        /// </summary>
        /// <param name="disposedAction"></param>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public IUnitOfWorkOperator CreateUnitOfWorkAsync(Action disposedAction, bool manualCommit)
        {
            return IsScopedNull()
                ? NullUnitOfWorkOperator.Instance
                : ScopedUnitOfWorkManager.CreateUnitOfWorkAsync(disposedAction, manualCommit);
        }
    }
}