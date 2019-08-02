using System.Data.Common;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType, DbTransaction transaction)
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
                    reader.Read();
                    return reader.ToExpandoObject();
                }
            }
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, Action<DbCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToExpandoObject();
                }
            }
        }

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText)
            => @this.ExecuteExpandoObject(cmdText, null, CommandType.Text, null);

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, DbTransaction transaction)
            => @this.ExecuteExpandoObject(cmdText, null, CommandType.Text, transaction);

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, CommandType commandType)
            => @this.ExecuteExpandoObject(cmdText, null, commandType, null);

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, CommandType commandType, DbTransaction transaction)
            => @this.ExecuteExpandoObject(cmdText, null, commandType, transaction);

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, DbParameter[] parameters)
            => @this.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, null);

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, DbParameter[] parameters, DbTransaction transaction)
            => @this.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, transaction);

        /// <summary>
        /// Execute expando object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static dynamic ExecuteExpandoObject(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType)
            => @this.ExecuteExpandoObject(cmdText, parameters, commandType, null);
    }
}