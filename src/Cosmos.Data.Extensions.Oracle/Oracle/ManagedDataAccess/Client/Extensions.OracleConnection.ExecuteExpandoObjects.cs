using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Cosmos;
using Cosmos.Data.Sx;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = command.ExecuteReader();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, Action<OracleCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = ((DbCommand) command).ExecuteReader();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, OracleParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjects(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#else
            await using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#endif
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, Action<OracleCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(commandFactory);
#else
            await using var command = conn.CreateCommand(commandFactory);
#endif
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, parameters, commandType, null);
        }
    }
}