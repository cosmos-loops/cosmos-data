using System;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Update
{
    public class UpdateAction<TEntity> : SQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public UpdateAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            TEntity entity,
            ISQLPredicate[] filters = null,
            bool ignoreAllKeyProperties = false)
            : base(rootActionSet, ActionKind.Update, contextParams, filters)
        {
            InternalCommand = (i, f) => _connector.Update(i, TransactionWrapper.CurrentTransaction, f, ignoreAllKeyProperties);
            EntityInstance = entity.DeepCopy();
            Filters = filters;
        }

        private TEntity EntityInstance { get; }

        private ISQLPredicate[] Filters { get; }

        private Action<TEntity, ISQLPredicate[]> InternalCommand { get; set; }

        public void Execute()
        {
            ActionBankGetter.RootActionBank.Execute();
        }

        void IExecutableSQLAction.ExecuteCalledFromBank()
        {
            if (IsExecuted)
                return;

            InternalCommand.Invoke(EntityInstance, Filters);
            IsExecuted = true;
        }
    }
}