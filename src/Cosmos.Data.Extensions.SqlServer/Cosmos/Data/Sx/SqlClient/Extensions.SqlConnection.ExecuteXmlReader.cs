using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml;

namespace Cosmos.Data.Sx.SqlClient
{
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteXmlReader();
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, Action<SqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteXmlReader();
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, SqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReader(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return command.ExecuteXmlReaderAsync();
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, Action<SqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return command.ExecuteXmlReaderAsync();
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<XmlReader> ExecuteXmlReaderAsync(this SqlConnection conn, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteXmlReaderAsync(cmdText, parameters, commandType, null);
        }
    }
}