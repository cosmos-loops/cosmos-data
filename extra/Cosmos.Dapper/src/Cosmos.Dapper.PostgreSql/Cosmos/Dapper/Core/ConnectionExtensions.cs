using System;
using Npgsql;

namespace Cosmos.Dapper.Core
{
    public static class ConnectionExtensions
    {
        public static NpgsqlConnection ToConn(this DapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            return new NpgsqlConnection(options.ConnectionString);
        }

        public static NpgsqlConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}