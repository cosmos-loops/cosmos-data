using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
// ReSharper disable once InconsistentNaming
    public abstract class SQLAction<TEntity> : SQLActionSyncBase where TEntity : class, IEntity, new()
    {
        protected SQLAction(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, contextParams, filters) { }
    }

}