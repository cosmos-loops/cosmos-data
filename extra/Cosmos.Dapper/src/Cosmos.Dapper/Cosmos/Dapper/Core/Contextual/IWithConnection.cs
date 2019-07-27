using System.Data;

namespace Cosmos.Dapper.Core.Contextual
{
    public interface IWithConnection<TConnection>
        where TConnection : class, IDbConnection
    {
        IDapperConnector<TConnection> Connector { get; }
    }
}