using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos;

namespace MySqlConnector
{
    public static partial class MySqlClientExtensions
    {
        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteScalar();
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteScalar();
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalar(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType,
            MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteScalarAsync();
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteScalarAsync();
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<object> ExecuteScalarAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsync(cmdText, parameters, commandType, null);
        }
    }
}