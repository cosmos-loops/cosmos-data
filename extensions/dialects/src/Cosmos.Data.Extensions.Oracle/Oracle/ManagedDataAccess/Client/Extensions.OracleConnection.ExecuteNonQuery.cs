using System;
using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType, OracleTransaction transaction)
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

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, Action<OracleCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText)
        {
            @this.ExecuteNonQuery(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, OracleTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, CommandType commandType)
        {
            @this.ExecuteNonQuery(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, OracleParameter[] parameters)
        {
            @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            @this.ExecuteNonQuery(cmdText, parameters, commandType, null);
        }
    }
}