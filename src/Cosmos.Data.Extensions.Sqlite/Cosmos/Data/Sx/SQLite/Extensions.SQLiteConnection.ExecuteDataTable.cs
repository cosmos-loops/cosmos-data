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
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType,
            SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteFirstDataTable();
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteFirstDataTable();
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTable(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType,
            SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteFirstDataTableAsync();
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteFirstDataTableAsync();
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<DataTable> ExecuteDataTableAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, parameters, commandType, null);
        }
    }
}