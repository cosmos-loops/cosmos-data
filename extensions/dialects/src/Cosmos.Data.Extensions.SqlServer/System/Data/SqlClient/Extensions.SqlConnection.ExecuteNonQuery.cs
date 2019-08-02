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
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
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
        public static void ExecuteNonQuery(this SqlConnection @this, Action<SqlCommand> commandFactory)
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
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText)
        {
            @this.ExecuteNonQuery(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, CommandType commandType)
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
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute NonQuery
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
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
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
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
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            @this.ExecuteNonQuery(cmdText, parameters, commandType, null);
        }
    }
}