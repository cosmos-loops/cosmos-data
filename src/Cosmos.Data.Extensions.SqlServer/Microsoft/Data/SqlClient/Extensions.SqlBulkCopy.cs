using System.Reflection;
using Cosmos;

#if NET451 || NET452
// ReSharper disable once CheckNamespace
namespace System.Data.SqlClient
{
#else
namespace Microsoft.Data.SqlClient
{
#endif

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