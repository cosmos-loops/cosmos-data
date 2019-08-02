using System.Data.Common;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType, DbTransaction transaction)
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
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, Action<DbCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteReader();
            }
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText)
            => @this.ExecuteReader(cmdText, null, CommandType.Text, null);

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, DbTransaction transaction)
            => @this.ExecuteReader(cmdText, null, CommandType.Text, transaction);

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, CommandType commandType)
            => @this.ExecuteReader(cmdText, null, commandType, null);

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, CommandType commandType, DbTransaction transaction)
            => @this.ExecuteReader(cmdText, null, commandType, transaction);

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, DbParameter[] parameters)
            => @this.ExecuteReader(cmdText, parameters, CommandType.Text, null);

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, DbParameter[] parameters, DbTransaction transaction)
            => @this.ExecuteReader(cmdText, parameters, CommandType.Text, transaction);

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType)
            => @this.ExecuteReader(cmdText, parameters, commandType, null);
    }
}