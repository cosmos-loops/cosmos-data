using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    internal static class NpgSqlBulkCopyHelper
    {
        internal static bool OpenIfNeeded(this NpgsqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                return true;
            }

            return false;
        }

        internal static async Task<bool> OpenIfNeededAsync(this NpgsqlConnection conn, CancellationToken cancellationToken = default)
        {
            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync(cancellationToken);
                return true;
            }

            return false;
        }

        internal static void CloseIfNeeded(this NpgsqlConnection conn, bool needAutoCloseConn)
        {
            if (conn is not null && needAutoCloseConn)
            {
                conn.Close();
            }
        }

#if NET452
        internal static Task CloseIfNeededAsync(this NpgsqlConnection conn, bool needAutoCloseConn)
        {
            if (conn is not null && needAutoCloseConn)
            {
                conn.Close();
            }

            return Asynchronous.Tasks.CompletedTask();
        }

#else
        internal static async Task CloseIfNeededAsync(this NpgsqlConnection conn, bool needAutoCloseConn)
        {
            if (conn is not null && needAutoCloseConn)
            {
                await conn.CloseAsync();
            }
        }
#endif
    }
}