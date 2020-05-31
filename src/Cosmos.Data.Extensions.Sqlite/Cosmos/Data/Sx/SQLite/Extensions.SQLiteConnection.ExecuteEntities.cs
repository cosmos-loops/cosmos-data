using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Cosmos.Data.Sx.SQLite
{
    /// <summary>
    /// Extensions for Sqlite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class SQLiteExtensions
    {
        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType,
            SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = command.ExecuteReader();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = command.ExecuteReader();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntities<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType,
            SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = await command.ExecuteReaderAsync();
            return reader.ToEntities<T>();
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entities
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<IEnumerable<T>> ExecuteEntitiesAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntitiesAsync<T>(cmdText, parameters, commandType, null);
        }
    }
}