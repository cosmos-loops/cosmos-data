using System;
using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction)
        {
            using (var command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteReader();
            }
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, Action<OracleCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteReader();
            }
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText)
        {
            return @this.ExecuteReader(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, OracleTransaction transaction)
        {
            return @this.ExecuteReader(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteReader(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            return @this.ExecuteReader(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, OracleParameter[] parameters)
        {
            return @this.ExecuteReader(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            return @this.ExecuteReader(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteReader(cmdText, parameters, commandType, null);
        }
    }
}