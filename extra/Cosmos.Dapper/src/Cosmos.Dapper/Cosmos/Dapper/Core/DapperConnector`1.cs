using System;
using System.Data;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    public class DapperConnector<TConnection> : DapperConnector, IDapperConnector<TConnection>
        where TConnection : class, IDbConnection
    {
        public DapperConnector(TConnection connection, IDapperMappingConfig config, ISQLGenerator sqlGenerator) 
            : base(connection, config, sqlGenerator)
        {
            RawConnectionType = typeof(TConnection);
        }

        public TConnection RawConnection => Connection as TConnection;

        public Type RawConnectionType { get; }
    }
}