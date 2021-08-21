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
        /// Create command
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SQLiteCommand CreateCommand(this SQLiteConnection conn,
            string cmdText, CommandType commandType, SQLiteTransaction transaction,
            params SQLiteParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));

            var command = conn.CreateCommand();
            command.CommandText = cmdText;
            command.CommandType = commandType;
            command.Transaction = transaction;

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        /// <summary>
        /// Create command
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static SQLiteCommand CreateCommand(this SQLiteConnection conn, Action<SQLiteCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            var command = conn.CreateCommand();
            commandFactory?.Invoke(command);
            return command;
        }
    }
}