using System;
using System.Data;
using System.Data.SqlClient;

namespace Cosmos.Data.Sx.SqlClient
{
    public static partial class SqlClientExtensions
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
        public static SqlCommand CreateCommand(this SqlConnection conn,
            string cmdText, CommandType commandType, SqlTransaction transaction,
            params SqlParameter[] parameters)
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
        public static SqlCommand CreateCommand(this SqlConnection conn, Action<SqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));

            var command = conn.CreateCommand();
            commandFactory?.Invoke(command);
            return command;
        }

        /// <summary>
        /// Ping
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool Ping(this SqlConnection conn)
        {
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select 1";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                if (conn.State != ConnectionState.Closed)
                    try
                    {
                        conn.Close();
                    }
                    catch
                    {
                        // ignored
                    }

                return false;
            }
        }
    }
}