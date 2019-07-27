using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Delete
{
    public class AsynchronousExpressionDeleteAction<TEntity> : AsynchronousSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public AsynchronousExpressionDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            Expression<Func<TEntity, bool>> predicateExpression,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (expr, f, t) => _connector.DeleteAsync(expr, TransactionWrapper.CurrentTransaction, f, t);
            PredicateExpression = predicateExpression;
            Filters = filters;
        }

        private Expression<Func<TEntity, bool>> PredicateExpression { get; }

        private ISQLPredicate[] Filters { get; }

        private Func<Expression<Func<TEntity, bool>>, ISQLPredicate[], CancellationToken, Task> InternalCommand { get; set; }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await ActionBankGetter.RootActionBank.ExecuteAsync(cancellationToken);
        }

        async Task IAsynchronousExecutableSQLAction.ExecuteCalledFromBankAsync(CancellationToken cancellationToken)
        {
            if (IsExecuted)
                return;

            await InternalCommand.Invoke(PredicateExpression, Filters, cancellationToken);
            IsExecuted = true;
        }
    }
}