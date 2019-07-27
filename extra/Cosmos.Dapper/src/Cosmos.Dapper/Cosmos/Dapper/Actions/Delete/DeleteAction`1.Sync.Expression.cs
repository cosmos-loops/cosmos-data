using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions.Delete
{
    public class ExpressionDeleteAction<TEntity> : SQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        public ExpressionDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            Expression<Func<TEntity, bool>> predicateExpression,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (expr, f) => _connector.Delete(expr, TransactionWrapper.CurrentTransaction, f);
            PredicateExpression = predicateExpression;
            Filters = filters;
        }

        private Expression<Func<TEntity, bool>> PredicateExpression { get; }

        private ISQLPredicate[] Filters { get; }

        private Action<Expression<Func<TEntity, bool>>, ISQLPredicate[]> InternalCommand { get; set; }

        public void Execute()
        {
            ActionBankGetter.RootActionBank.Execute();
        }

        void IExecutableSQLAction.ExecuteCalledFromBank()
        {
            if (IsExecuted)
                return;

            InternalCommand.Invoke(PredicateExpression, Filters);
            IsExecuted = true;
        }
    }
}