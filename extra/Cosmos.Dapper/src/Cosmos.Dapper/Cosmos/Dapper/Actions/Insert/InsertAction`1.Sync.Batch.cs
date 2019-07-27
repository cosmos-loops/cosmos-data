using System;
using System.Collections.Generic;
using Cosmos.Dapper.Core;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Insert
{
    public class BatchInsertAction<TEntity> : BatchSQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public BatchInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, IEnumerable<TEntity> entities)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = c => _connector.Insert(c, TransactionWrapper.CurrentTransaction);
            EntityInstanceColl = entities.DeepCopy();
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private Action<IEnumerable<TEntity>> InternalCommand { get; set; }

        public void Execute()
        {
            ActionBankGetter.RootActionBank.Execute();
        }

        void IExecutableSQLAction.ExecuteCalledFromBank()
        {
            if (IsExecuted)
                return;

            InternalCommand.Invoke(EntityInstanceColl);
            IsExecuted = true;
        }
    }
}