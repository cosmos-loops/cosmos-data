using System;
using System.Data;
using Cosmos.Data.Common;
using Cosmos.Data.Common.UnitOfWork;

namespace Cosmos.Data.Core.UnitOfWork
{
    internal partial class UnitOfWorkManagerHolder : IUnitOfWorkManager
    {
        internal UnitOfWorkManager HoldingManager { get; private set; }

        private UnitOfWorkManagerHolder()
        {
            HoldingManager = new(null, null, false);
        }

        public void Dispose()
        {
            HoldingManager = null;
        }

        public IUnitOfWorkEntry Current => HoldingManager.Current;

        public void Binding(IRepository repository) => HoldingManager.Binding(repository);

        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => HoldingManager.CreateUnitOfWork(transWrapper, types, level);

        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposableAction,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => HoldingManager.CreateUnitOfWork(transWrapper, disposableAction, types, level);

        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => HoldingManager.CreateUnitOfWork(transWrapper, manualCommit, types, level);

        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposableAction, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => HoldingManager.CreateUnitOfWork(transWrapper, disposableAction, manualCommit, types, level);

        public bool IsVirtualManager => true;

        public object GetAdditionalData(string keyOfData) => HoldingManager.GetAdditionalData(keyOfData);

        public TData GetAdditionalData<TData>() => HoldingManager.GetAdditionalData<TData>();

        public TData GetAdditionalData<TData>(string keyOfTypedData) => HoldingManager.GetAdditionalData<TData>(keyOfTypedData);

        public void SetAdditionalData(string keyOfData, object data) => HoldingManager.SetAdditionalData(keyOfData, data);

        public void SetAdditionalData<TData>(TData data) => HoldingManager.SetAdditionalData(data);

        public void SetAdditionalData<TData>(string keyOfData, TData data) => HoldingManager.SetAdditionalData(keyOfData, data);
    }
}