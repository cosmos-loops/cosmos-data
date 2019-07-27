using System;
using System.Data.SqlClient;

namespace Cosmos.Dapper.Core
{
    public static class ConnectionExtensions
    {
        public static SqlConnection ToConn(this DapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            return new SqlConnection(options.ConnectionString);
        }

        public static SqlConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}