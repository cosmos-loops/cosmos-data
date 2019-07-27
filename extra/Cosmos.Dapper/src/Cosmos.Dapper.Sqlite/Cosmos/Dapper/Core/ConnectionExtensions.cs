using System;
using System.Data.SQLite;

namespace Cosmos.Dapper.Core
{
    public static class ConnectionExtensions
    {
        public static SQLiteConnection ToConn(this DapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            return new SQLiteConnection(options.ConnectionString);
        }

        public static SQLiteConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}