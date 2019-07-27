using System;
using Cosmos.Data.Store;
using Cosmos.Data.UnitOfWork;

namespace Cosmos.Data
{
    public static class UnitOfWorkExtensions
    {
        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager)
        {
            return new UnitOfWorkOperator(manager);
        }

        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, manualCommit);
        }

        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager, Action disposedAction)
        {
            return new UnitOfWorkOperator(manager, disposedAction);
        }

        public static IUnitOfWorkOperator CreateUnitOfWork(this IStoreContextManager manager, Action disposedAction, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, disposedAction, manualCommit);
        }

        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager)
        {
            return new UnitOfWorkOperator(manager, isAsynchronous: true);
        }

        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, manualCommit, isAsynchronous: true);
        }

        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager, Action disposedAction)
        {
            return new UnitOfWorkOperator(manager, disposedAction, isAsynchronous: true);
        }

        public static IUnitOfWorkOperator CreateUnitOfWorkAsync(this IStoreContextManager manager, Action disposedAction, bool manualCommit)
        {
            return new UnitOfWorkOperator(manager, disposedAction, manualCommit, isAsynchronous: true);
        }
    }
}