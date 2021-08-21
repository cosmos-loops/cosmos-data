using System.Threading.Tasks;
using Cosmos;

namespace System.Data.SQLite
{
    /// <summary>
    /// Extensions for Sqlite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class SQLiteExtensions
    {
        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            var ds = new DataSet();
            using var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            
            using var command = conn.CreateCommand(commandFactory);
            var ds = new DataSet();
            using var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataSet.</returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType,
            SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteDataSetAsync();
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteDataSetAsync();
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, CommandType commandType, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, SQLiteTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this SQLiteConnection conn, string cmdText, SQLiteParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, parameters, commandType, null);
        }
    }
}