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
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, CommandType commandType, MySqlTransaction transaction)
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

                var ds = new DataSet();
                using (var dataAdapter = new MySqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }

                return ds;
            }
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, Action<MySqlCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                var ds = new DataSet();
                using (var dataAdapter = new MySqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }

                return ds;
            }
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText)
        {
            return @this.ExecuteDataSet(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, MySqlTransaction transaction)
        {
            return @this.ExecuteDataSet(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteDataSet(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            return @this.ExecuteDataSet(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters)
        {
            return @this.ExecuteDataSet(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            return @this.ExecuteDataSet(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlConnection @this, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteDataSet(cmdText, parameters, commandType, null);
        }
    }
}