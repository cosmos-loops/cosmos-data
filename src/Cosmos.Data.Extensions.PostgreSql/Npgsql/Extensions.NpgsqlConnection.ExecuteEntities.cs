using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cosmos;
using Cosmos.Data.Sx;

namespace Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
    {
        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType,
            NpgsqlTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = command.ExecuteReader();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = command.ExecuteReader();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, NpgsqlTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, CommandType commandType, NpgsqlTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType,
            NpgsqlTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#else
            await using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#endif
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory) where T : new()
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(commandFactory);
#else
            await using var command = conn.CreateCommand(commandFactory);
#endif
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, NpgsqlTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, CommandType commandType, NpgsqlTransaction transaction)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, parameters, commandType, null);
        }
    }
}