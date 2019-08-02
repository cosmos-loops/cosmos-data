using System;
using System.Collections.Generic;
using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType,
            OracleTransaction transaction) where T : new()
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

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToEntities<T>();
                }
            }
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, Action<OracleCommand> commandFactory) where T : new()
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToEntities<T>();
                }
            }
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, OracleTransaction transaction) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, CommandType commandType) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, CommandType commandType, OracleTransaction transaction) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, OracleTransaction transaction) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute a set of entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteEntities<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, commandType, null);
        }
    }
}