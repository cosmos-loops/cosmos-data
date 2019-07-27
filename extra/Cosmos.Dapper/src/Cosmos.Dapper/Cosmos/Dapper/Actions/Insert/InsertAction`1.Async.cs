using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Insert
{
    public class AsynchronousInsertAction<TEntity> : AsynchronousSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public AsynchronousInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, TEntity entity)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = (i, t) => _connector.InsertAsync(i, TransactionWrapper.CurrentTransaction, t);
            EntityInstance = entity.DeepCopy();
        }

        private TEntity EntityInstance { get; }

        private Func<TEntity, CancellationToken, Task> InternalCommand { get; set; }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await ActionBankGetter.RootActionBank.ExecuteAsync(cancellationToken);
        }

        async Task IAsynchronousExecutableSQLAction.ExecuteCalledFromBankAsync(CancellationToken cancellationToken)
        {
            if (IsExecuted)
                return;

            await InternalCommand.Invoke(EntityInstance, cancellationToken);
            IsExecuted = true;
        }
    }
}