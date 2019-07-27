using System;
using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
          public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType, OracleTransaction transaction)
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

                return (T) command.ExecuteScalar();
            }
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, Action<OracleCommand> commandFactory)
        {
            using (OracleCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return (T) command.ExecuteScalar();
            }
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, null);
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, OracleTransaction transaction)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, transaction);
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, commandType, null);
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, CommandType commandType, OracleTransaction transaction)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, commandType, transaction);
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters)
        {
            return @this.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, null);
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, OracleTransaction transaction)
        {
            return @this.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        public static T ExecuteScalarAs<T>(this OracleConnection @this, string cmdText, OracleParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteScalarAs<T>(cmdText, parameters, commandType, null);
        }
    }
}