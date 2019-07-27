using System;
using System.Data;

namespace Cosmos.Dapper.Core
{
    public interface IDapperConnector<out TConnection> : IDapperConnector where TConnection : class, IDbConnection
    {
        TConnection RawConnection { get; }

        Type RawConnectionType { get; }
    }
}
