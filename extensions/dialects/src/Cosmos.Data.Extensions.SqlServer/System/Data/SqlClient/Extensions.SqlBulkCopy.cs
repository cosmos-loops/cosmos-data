using System.Reflection;

namespace System.Data.SqlClient
{
    /// <summary>
    /// Extensions for <see cref="SqlClient"/>
    /// </summary>
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Gets connection
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static SqlConnection GetConnection(this SqlBulkCopy @this)
        {
            var type = @this.GetType();
            var field = type.GetField("_connection", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(@this) as SqlConnection;
        }

        /// <summary>
        /// Get transaction
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static SqlTransaction GetTransaction(this SqlBulkCopy @this)
        {
            var type = @this.GetType();
            var field = type.GetField("_externalTransaction", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(@this) as SqlTransaction;
        }
    }
}