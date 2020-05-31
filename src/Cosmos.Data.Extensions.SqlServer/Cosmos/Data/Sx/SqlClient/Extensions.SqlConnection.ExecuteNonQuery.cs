using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cosmos.Data.Sx.SqlClient
{
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, Action<SqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, SqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, Action<SqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        public static Task ExecuteNonQueryAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, parameters, commandType, null);
        }
    }
}