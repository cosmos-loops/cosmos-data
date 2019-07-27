using System;
using Cosmos.Dapper.Core;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Insert
{
    public class InsertAction<TEntity> : SQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public InsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, TEntity entity)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = i => _connector.Insert(i, TransactionWrapper.CurrentTransaction);
            EntityInstance = entity.DeepCopy();
        }

        private TEntity EntityInstance { get; }

        private Action<TEntity> InternalCommand { get; set; }

        public void Execute()
        {
            ActionBankGetter.RootActionBank.Execute();
        }

        void IExecutableSQLAction.ExecuteCalledFromBank()
        {
            if (IsExecuted)
                return;

            InternalCommand.Invoke(EntityInstance);
            IsExecuted = true;
        }
    }
}