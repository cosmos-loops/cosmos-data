using Cosmos;

#if NET451 || NET452
// ReSharper disable once CheckNamespace
namespace System.Data.SqlClient
{
#else
using System;
using System.Data;

namespace Microsoft.Data.SqlClient
{
#endif

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