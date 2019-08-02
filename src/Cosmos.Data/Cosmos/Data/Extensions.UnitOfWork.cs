using System;
using Cosmos.Data.Store;
using Cosmos.Data.UnitOfWork;

namespace Cosmos.Data
{
    /// <summary>
    /// Extensions for UnitOfWork
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/>.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager)
        {
            return new UnitOfWorkOperator(manager);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/>.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, manualCommit);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/>.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="disposedAction"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager, Action disposedAction)
        {
            return new UnitOfWorkOperator(manager, disposedAction);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/>.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="disposedAction"></param>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager, Action disposedAction, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, disposedAction, manualCommit);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/> async.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager)
        {
            return new UnitOfWorkOperator(manager, isAsynchronous: true);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/> async.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, manualCommit, isAsynchronous: true);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/> async.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="disposedAction"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager, Action disposedAction)
        {
            return new UnitOfWorkOperator(manager, disposedAction, isAsynchronous: true);
        }

        /// <summary>
        /// Create a new instance of the implementation of <see cref="IUnitOfWorkOperator"/> async.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="disposedAction"></param>
        /// <param name="manualCommit"></param>
        /// <returns></returns>
        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager, Action disposedAction, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, disposedAction, manualCommit, isAsynchronous: true);
        }
    }
}