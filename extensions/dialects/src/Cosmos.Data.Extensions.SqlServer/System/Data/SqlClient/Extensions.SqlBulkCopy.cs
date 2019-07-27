using System.Reflection;

namespace System.Data.SqlClient
{
    public static partial class SqlClientExtensions
    {
        public static SqlConnection GetConnection(this SqlBulkCopy @this)
        {
            var type = @this.GetType();
            var field = type.GetField("_connection", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(@this) as SqlConnection;
        }

        public static SqlTransaction GetTransaction(this SqlBulkCopy @this)
        {
            var type = @this.GetType();
            var field = type.GetField("_externalTransaction", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(@this) as SqlTransaction;
        }
    }
}