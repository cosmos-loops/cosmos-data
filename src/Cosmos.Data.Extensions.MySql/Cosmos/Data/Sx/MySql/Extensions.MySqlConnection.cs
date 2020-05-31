using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Cosmos.Data.Sx.MySql
{
    public static partial class MySqlClientExtensions
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
        public static MySqlCommand CreateCommand(this MySqlConnection conn,
            string cmdText, CommandType commandType, MySqlTransaction transaction,
            params MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));

            var command = conn.CreateCommand();
            command.CommandText = cmdText;
            command.CommandType = commandType;
            command.Transaction = transaction;

            if (parameters != null)
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
        public static MySqlCommand CreateCommand(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));

            var command = conn.CreateCommand();
            commandFactory?.Invoke(command);
            return command;
        }
    }
}