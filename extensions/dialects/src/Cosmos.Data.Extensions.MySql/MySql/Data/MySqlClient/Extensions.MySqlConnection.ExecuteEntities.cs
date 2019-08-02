// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Data;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// Extensions for <see cref="MySqlClient"/>
    /// </summary>
    public static partial class MySqlClientExtensions
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, CommandType commandType,
            MySqlTransaction transaction) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, Action<MySqlCommand> commandFactory) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, MySqlTransaction transaction) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, CommandType commandType) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, CommandType commandType, MySqlTransaction transaction) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction) where T : new()
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
        public static IEnumerable<T> ExecuteEntities<T>(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, CommandType commandType) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, commandType, null);
        }
    }
}