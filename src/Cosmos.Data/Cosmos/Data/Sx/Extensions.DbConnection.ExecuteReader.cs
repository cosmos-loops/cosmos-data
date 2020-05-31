using System;
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
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, Action<DbCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, CommandType commandType, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, DbParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, DbParameter[] parameters, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReader(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteReaderAsync();
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, Action<DbCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteReaderAsync();
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, CommandType commandType, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, DbParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, DbParameter[] parameters, DbTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection conn, string cmdText, DbParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, parameters, commandType, null);
        }
    }
}