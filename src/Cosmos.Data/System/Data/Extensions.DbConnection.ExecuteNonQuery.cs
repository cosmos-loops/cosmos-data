using System.Data.Common;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType, DbTransaction transaction)
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
        public static void ExecuteNonQuery(this DbConnection @this, Action<DbCommand> commandFactory)
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
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText)
            => @this.ExecuteNonQuery(cmdText, null, CommandType.Text, null);

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, DbTransaction transaction)
            => @this.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, CommandType commandType)
            => @this.ExecuteNonQuery(cmdText, null, commandType, null);

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, CommandType commandType, DbTransaction transaction)
            => @this.ExecuteNonQuery(cmdText, null, commandType, transaction);

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, DbParameter[] parameters)
            => @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, null);

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, DbParameter[] parameters, DbTransaction transaction)
            => @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, transaction);

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType)
            => @this.ExecuteNonQuery(cmdText, parameters, commandType, null);

    }
}