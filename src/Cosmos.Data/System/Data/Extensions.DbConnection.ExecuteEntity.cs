using System.Data.Common;

namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="DbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType, DbTransaction transaction) where T : new()
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
                    return reader.ToEntity<T>();
                }
            }
        }

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, Action<DbCommand> commandFactory) where T : new()
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToEntity<T>();
                }
            }
        }

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText) where T : new()
            => @this.ExecuteEntity<T>(cmdText, null, CommandType.Text, null);

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, DbTransaction transaction) where T : new()
            => @this.ExecuteEntity<T>(cmdText, null, CommandType.Text, transaction);

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, CommandType commandType) where T : new()
            => @this.ExecuteEntity<T>(cmdText, null, commandType, null);

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, CommandType commandType, DbTransaction transaction) where T : new()
            => @this.ExecuteEntity<T>(cmdText, null, commandType, transaction);

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, DbParameter[] parameters) where T : new()
            => @this.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, null);

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, DbParameter[] parameters, DbTransaction transaction) where T : new()
            => @this.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, transaction);

        /// <summary>
        /// Execute entity
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteEntity<T>(this DbConnection @this, string cmdText, DbParameter[] parameters, CommandType commandType) where T : new()
            => @this.ExecuteEntity<T>(cmdText, parameters, commandType, null);
    }
}