using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, Action<OracleCommand> commandFactory)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, OracleTransaction transaction)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, CommandType commandType)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, CommandType commandType, OracleTransaction transaction)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, OracleParameter[] parameters)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
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
        public static DataTable ExecuteDataTable(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, Action<OracleCommand> commandFactory)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, OracleTransaction transaction)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, CommandType commandType)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, CommandType commandType, OracleTransaction transaction)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
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
        public static Task<DataTable> ExecuteDataTableAsync(this OracleConnection conn, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteDataTableAsync(cmdText, parameters, commandType, null);
        }
    }
}