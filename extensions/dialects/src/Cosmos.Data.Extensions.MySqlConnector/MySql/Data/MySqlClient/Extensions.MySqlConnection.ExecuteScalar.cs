// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System;
using System.Data;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// Extensions for <see cref="MySqlClient"/>
    /// </summary>
    public static partial class MySqlClientExtensions
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, CommandType commandType, MySqlTransaction transaction)
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
        public static object ExecuteScalar(this MySqlConnection @this, Action<MySqlCommand> commandFactory)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, MySqlTransaction transaction)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, CommandType commandType)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, CommandType commandType, MySqlTransaction transaction)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
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
        public static object ExecuteScalar(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteScalar(cmdText, parameters, commandType, null);
        }
    }
}