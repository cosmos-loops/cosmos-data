using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos;

namespace Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
    {
        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType, NpgsqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteDataSet();
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteDataSet();
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText)
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
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, NpgsqlTransaction transaction)
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
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, CommandType commandType)
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
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, CommandType commandType, NpgsqlTransaction transaction)
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
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters)
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
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction)
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
        public static DataSet ExecuteDataSet(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSet(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType,
            NpgsqlTransaction transaction)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, Action<NpgsqlCommand> commandFactory)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, NpgsqlTransaction transaction)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, CommandType commandType)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, CommandType commandType, NpgsqlTransaction transaction)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, NpgsqlTransaction transaction)
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
        public static Task<DataSet> ExecuteDataSetAsync(this NpgsqlConnection conn, string cmdText, NpgsqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataSetAsync(cmdText, parameters, commandType, null);
        }
    }
}