using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Insert
{
    public class AsynchronousBulkInsertAction<TEntity> : AsynchronousBatchSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public AsynchronousBulkInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, IEnumerable<TEntity> entities)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            var bulkInsertOperator = contextParams.GetBulkInsertOperator(_connector);
            InternalCommand = async (c, t) => await bulkInsertOperator.ProcessAsync(c.ToList());
            EntityInstanceColl = entities.DeepCopy();
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private Func<IEnumerable<TEntity>, CancellationToken, Task> InternalCommand { get; set; }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await ActionBankGetter.RootActionBank.ExecuteAsync(cancellationToken);
        }

        async Task IAsynchronousExecutableSQLAction.ExecuteCalledFromBankAsync(CancellationToken cancellationToken)
        {
            if (IsExecuted)
                return;

            await InternalCommand.Invoke(EntityInstanceColl, cancellationToken);
            IsExecuted = true;
        }
    }
}