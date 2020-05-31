using System;
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
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            conn.ExecuteNonQuery(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        public static Task ExecuteNonQueryAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteNonQueryAsync(cmdText, parameters, commandType, null);
        }
    }
}