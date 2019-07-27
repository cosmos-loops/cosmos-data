using System;
using MySql.Data.MySqlClient;

namespace Cosmos.Dapper.Core
{
    public static class ConnectionExtensions
    {
        public static MySqlConnection ToConn(this DapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            return new MySqlConnection(options.ConnectionString);
        }

        public static MySqlConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}