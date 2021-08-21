using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos;
using Cosmos.Conversions;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = CreateCommand(conn, cmdText, commandType, transaction, parameters);
            return command.ExecuteScalar().CastTo<T>();
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, Action<OracleCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteScalar().CastTo<T>();
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarTo<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = CreateCommand(conn, cmdText, commandType, transaction, parameters);
#else
            await using var command = CreateCommand(conn, cmdText, commandType, transaction, parameters);
#endif
            return (await command.ExecuteScalarAsync()).CastTo<T>();
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, Action<OracleCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(commandFactory);
#else
            await using var command = conn.CreateCommand(commandFactory);
#endif
            return (await command.ExecuteScalarAsync()).CastTo<T>();
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarToAsync<T>(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarToAsync<T>(cmdText, parameters, commandType, null);
        }
    }
}