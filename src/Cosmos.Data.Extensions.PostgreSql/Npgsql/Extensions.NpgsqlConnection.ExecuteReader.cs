using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos;

#pragma warning disable 1998

namespace Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType,
            NpgsqlTransaction transaction)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, NpgsqlTransaction transaction)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, CommandType commandType)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, CommandType commandType, NpgsqlTransaction transaction)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction)
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
        public static NpgsqlDataReader ExecuteReader(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType)
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
        public static async Task<NpgsqlDataReader> ExecuteReaderAsync(
            this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType, NpgsqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#else
            await using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#endif

#if NET452
            return command.ExecuteReader();
#else
            return await command.ExecuteReaderAsync();
#endif
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static async Task<NpgsqlDataReader> ExecuteReaderAsync(
            this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(commandFactory);
#else
            await using var command = conn.CreateCommand(commandFactory);
#endif

#if NET452
            return command.ExecuteReader();
#else
            return await command.ExecuteReaderAsync();
#endif
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText)
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
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText, NpgsqlTransaction transaction)
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
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText, CommandType commandType)
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
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText, CommandType commandType, NpgsqlTransaction transaction)
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
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters)
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
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction)
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
        public static Task<NpgsqlDataReader> ExecuteReaderAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteReaderAsync(cmdText, parameters, commandType, null);
        }
    }
}