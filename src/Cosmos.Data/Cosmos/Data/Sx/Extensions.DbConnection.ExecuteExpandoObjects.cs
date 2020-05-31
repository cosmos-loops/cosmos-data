using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="DbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType,
            DbTransaction transaction)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, Action<DbCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = command.ExecuteReader();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, DbTransaction transaction)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, CommandType commandType)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, CommandType commandType, DbTransaction transaction)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, DbParameter[] parameters)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, DbParameter[] parameters, DbTransaction transaction)
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
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType)
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
        public static async Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType,
            DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, Action<DbCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToExpandoObjects();
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText)
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
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, DbTransaction transaction)
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
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, CommandType commandType)
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
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, CommandType commandType, DbTransaction transaction)
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
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, DbParameter[] parameters)
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
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, DbParameter[] parameters, DbTransaction transaction)
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
        public static Task<IEnumerable<dynamic>> ExecuteExpandoObjectsAsync(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteExpandoObjectsAsync(cmdText, parameters, commandType, null);
        }
    }
}