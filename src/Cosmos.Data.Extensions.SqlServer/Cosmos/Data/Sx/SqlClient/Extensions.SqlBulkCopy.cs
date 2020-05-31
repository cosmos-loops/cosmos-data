using System.Data.SqlClient;
using System.Reflection;

namespace Cosmos.Data.Sx.SqlClient
{
    /// <summary>
    /// Extensions for <see cref="SqlClient"/>
    /// </summary>
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Gets connection
        /// </summary>
        /// <param name="bulk"></param>
        /// <returns></returns>
        public static SqlConnection GetConnection(this SqlBulkCopy bulk)
        {
            bulk.CheckNull(nameof(bulk));
            var type = bulk.GetType();
            var field = type.GetField("_connection", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(bulk) as SqlConnection;
        }

        /// <summary>
        /// Get transaction
        /// </summary>
        /// <param name="bulk"></param>
        /// <returns></returns>
        public static SqlTransaction GetTransaction(this SqlBulkCopy bulk)
        {
            bulk.CheckNull(nameof(bulk));
            var type = bulk.GetType();
            var field = type.GetField("_externalTransaction", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(bulk) as SqlTransaction;
        }
    }
}