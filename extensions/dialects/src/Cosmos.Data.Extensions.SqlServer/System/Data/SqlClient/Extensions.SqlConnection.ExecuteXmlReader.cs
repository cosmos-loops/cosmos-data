// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System.Xml;

namespace System.Data.SqlClient
{
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
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

                return command.ExecuteXmlReader();
            }
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFactory"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (var command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteXmlReader();
            }
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteXmlReader(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteXmlReader(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteXmlReader(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, CommandType commandType, SqlTransaction transaction)
        {
            return @this.ExecuteXmlReader(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteXmlReader(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters, SqlTransaction transaction)
        {
            return @this.ExecuteXmlReader(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute xml reader
        /// </summary>
        /// <param name="this"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteXmlReader(cmdText, parameters, commandType, null);
        }
    }
}