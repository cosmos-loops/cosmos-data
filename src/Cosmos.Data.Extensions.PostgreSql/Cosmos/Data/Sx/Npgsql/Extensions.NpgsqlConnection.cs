using System;
using System.Data;
using Npgsql;

namespace Cosmos.Data.Sx.Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
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
        public static NpgsqlCommand CreateCommand(this NpgsqlConnection conn,
            string cmdText, CommandType commandType, NpgsqlTransaction transaction,
            params NpgsqlParameter[] parameters)
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
        public static NpgsqlCommand CreateCommand(this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            var command = conn.CreateCommand();
            commandFactory?.Invoke(command);
            return command;
        }

        /// <summary>
        /// Ping
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public static bool Ping(this NpgsqlConnection that)
        {
            try
            {
                var cmd = that.CreateCommand();
                cmd.CommandText = "select 1";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                try
                {
                    that.Close();
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