using System;
using System.Data;
using Cosmos;

namespace Oracle.ManagedDataAccess.Client
{
    /// <summary>
    /// Extensions for OracleClient
    /// </summary>
    public static partial class OracleClientExtensions
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
        public static OracleCommand CreateCommand(this OracleConnection conn,
            string cmdText, CommandType commandType, OracleTransaction transaction,
            params OracleParameter[] parameters)
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
        public static OracleCommand CreateCommand(this OracleConnection conn, Action<OracleCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            var command = conn.CreateCommand();
            commandFactory?.Invoke(command);
            return command;
        }
    }
}