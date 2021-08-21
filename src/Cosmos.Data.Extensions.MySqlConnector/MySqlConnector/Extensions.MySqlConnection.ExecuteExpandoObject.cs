using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Cosmos;
using Cosmos.Data.Sx;

namespace MySqlConnector
{
    public static partial class MySqlClientExtensions
    {
        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.ToExpandoObject();
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = ((DbCommand) command).ExecuteReader();
            reader.Read();
            return reader.ToExpandoObject();
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObject(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType,
            MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = await command.ExecuteReaderAsync();
            reader.Read();
            return reader.ToExpandoObject();
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static async Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = await command.ExecuteReaderAsync();
            reader.Read();
            return reader.ToExpandoObject();
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<dynamic> ExecuteExpandoObjectAsync(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectAsync(cmdText, parameters, commandType, null);
        }
    }
}