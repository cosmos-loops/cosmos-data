using System;
using System.Data.SqlClient;
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;
using Microsoft.Data.SqlClient;

namespace ConnPoolTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "server={ip_address};database={dn_name};uid={user_name};pwd={password};";

            ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Register();
            ConnectionPool.Pools.Register<SqlConnection, SqlConnectionPool>(
                () => new SqlConnectionPool("Name", connectionString, null, null),
                connectionString);

            var pool = ObjectPoolManager.Managed<ConnectionPoolManagedModel>.Get<SqlConnection>(connectionString);

            var connOut = pool.Acquire();

            var conn = connOut.Value;

            if (conn is null)
            {
                Console.WriteLine("CONN is null!");
            }

            pool.Recycle(connOut);

            Console.WriteLine("Hello World!");
        }
    }
}