using System.Threading.Tasks;
using Cosmos;
using Cosmos.Data.Sx;

namespace System.Data.SQLite
{
    /// <summary>
    /// Extensions for Sqlite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class SQLiteExtensions
    {
        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType, SQLiteTransaction transaction)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            using IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.ToEntity<T>();
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory) where T : new()
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            using IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.ToEntity<T>();
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntity<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType,
            SQLiteTransaction transaction)
            where T : new()
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#else
            await using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#endif
            using IDataReader reader = await ExecuteReaderAsync(command);
            reader.Read();
            return reader.ToEntity<T>();
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory) where T : new()
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(commandFactory);
#else
            await using var command = conn.CreateCommand(commandFactory);
#endif
            using IDataReader reader = await ExecuteReaderAsync(command);
            reader.Read();
            return reader.ToEntity<T>();
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Entity
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteEntityAsync<T>(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType) where T : new()
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteEntityAsync<T>(cmdText, parameters, commandType, null);
        }
    }
}