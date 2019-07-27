using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Delete
{
    public class AsynchronousBatchDeleteAction<TEntity> : AsynchronousBatchSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public AsynchronousBatchDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            IEnumerable<TEntity> entities,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (c, f, t) => _connector.DeleteAsync(c, TransactionWrapper.CurrentTransaction, f, t);
            EntityInstanceColl = entities.DeepCopy();
            Filters = filters;
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private ISQLPredicate[] Filters { get; }

        private Func<IEnumerable<TEntity>, ISQLPredicate[], CancellationToken, Task> InternalCommand { get; set; }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await ActionBankGetter.RootActionBank.ExecuteAsync(cancellationToken);
        }

        async Task IAsynchronousExecutableSQLAction.ExecuteCalledFromBankAsync(CancellationToken cancellationToken)
        {
            if (IsExecuted)
                return;

            await InternalCommand.Invoke(EntityInstanceColl, Filters, cancellationToken);
            IsExecuted = true;
        }
    }
}