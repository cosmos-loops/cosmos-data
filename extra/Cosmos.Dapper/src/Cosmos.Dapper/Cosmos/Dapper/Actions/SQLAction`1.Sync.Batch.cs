using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public abstract class BatchSQLAction<TEntity> : SQLActionSyncBase where TEntity : class, IEntity, new()
    {
        protected BatchSQLAction(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, contextParams, filters) { }
    }

}