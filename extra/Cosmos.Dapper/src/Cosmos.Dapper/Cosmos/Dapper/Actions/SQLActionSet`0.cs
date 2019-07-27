using Cosmos.Dapper.Core;
using Dapper;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public class SQLActionSet : SQLActionSetBase
    {
        public SQLActionSet(IDapperConnector connector, IDapperMappingConfig config)
            : base(connector, config) { }
    }
}