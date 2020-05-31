using System.Data;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="IDbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Ensure open
        /// </summary>
        /// <param name="conn"></param>
        public static void EnsureOpen(this IDbConnection conn)
        {
            conn.CheckNull(nameof(conn));
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        /// <summary>
        /// Is connection open
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool IsConnectionOpen(this IDbConnection conn)
        {
            conn.CheckNull(nameof(conn));
            return conn.State == ConnectionState.Open;
        }

        /// <summary>
        /// Is connection not open
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool IsNotConnectionOpen(this IDbConnection conn)
        {
            conn.CheckNull(nameof(conn));
            return conn.State != ConnectionState.Open;
        }
    }
}