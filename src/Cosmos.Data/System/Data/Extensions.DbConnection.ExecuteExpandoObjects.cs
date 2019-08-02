using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType,
            DbTransaction transaction)
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
                    return reader.ToExpandoObjects();
                }
            }
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, Action<DbCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToExpandoObjects();
                }
            }
        }

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText)
            => @this.ExecuteExpandoObjects(cmdText, null, CommandType.Text, null);

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, DbTransaction transaction)
            => @this.ExecuteExpandoObjects(cmdText, null, CommandType.Text, transaction);

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, CommandType commandType)
            => @this.ExecuteExpandoObjects(cmdText, null, commandType, null);

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, CommandType commandType, DbTransaction transaction)
            => @this.ExecuteExpandoObjects(cmdText, null, commandType, transaction);

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, DbParameter[] parameters)
            => @this.ExecuteExpandoObjects(cmdText, parameters, CommandType.Text, null);

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, DbParameter[] parameters, DbTransaction transaction)
            => @this.ExecuteExpandoObjects(cmdText, parameters, CommandType.Text, transaction);

        /// <summary>
        /// Execute a set of expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType)
            => @this.ExecuteExpandoObjects(cmdText, parameters, commandType, null);

    }
}