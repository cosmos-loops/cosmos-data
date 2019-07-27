using System;
using Oracle.ManagedDataAccess.Client;

namespace Cosmos.Dapper.Core
{
    public static class ConnectionExtensions
    {
        public static OracleConnection ToConn(this DapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            return new OracleConnection(options.ConnectionString);
        }

        public static OracleConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}