using System;
using System.Data;
using Cosmos;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType, OracleTransaction transaction)
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

                return command.ExecuteScalar().CastTo<T>();
            }
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, Action<OracleCommand> commandFactory)
        {
            using (OracleCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteScalar().CastTo<T>();
            }
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, OracleTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar to...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarTo<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, commandType, null);
        }
    }
}