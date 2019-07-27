using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Dapper.Core;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Insert
{
    public class BulkInsertAction<TEntity> : BatchSQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public BulkInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, IEnumerable<TEntity> entities)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            var bulkInsertOperator = contextParams.GetBulkInsertOperator(_connector);
            InternalCommand = c => bulkInsertOperator.Process(c.ToList());
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