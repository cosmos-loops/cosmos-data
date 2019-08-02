// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

namespace System.Data.SqlClient
{
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
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

                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteScalar(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteScalar(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalar(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            return @this.ExecuteScalar(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteScalar(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
        {
            return @this.ExecuteScalar(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteScalar(cmdText, parameters, commandType, null);
        }
    }
}