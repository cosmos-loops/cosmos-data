using Cosmos.Dapper.Core;
using Cosmos.Domain.Core;
using Dapper;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public class SQLActionSet<TEntity> : SQLActionSetBase where TEntity : class, IEntity, new()
    {
        public SQLActionSet(IDapperConnector connector, IDapperMappingConfig config)
            : base(connector, config) { }
    }
}