using System.Data;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperCommandOperator
    {

        #region  Dapper Proxy Members

        public int Execute(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Execute(sql, param, Transaction, Options.Timeout, commandType);
        }

        public int Execute(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.Execute(InjectTransaction(command));
        }

        public object ExecuteScalar(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar(sql, param, Transaction, Options.Timeout, commandType);
        }

        public T ExecuteScalar<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        public object ExecuteScalar(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar(InjectTransaction(command));
        }

        public T ExecuteScalar<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar<T>(InjectTransaction(command));
        }

        public IDataReader ExecuteReader(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteReader(sql, param, Transaction, Options.Timeout, commandType);
        }

        public IDataReader ExecuteReader(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteReader(InjectTransaction(command));
        }

        public IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteReader(InjectTransaction(command), commandBehavior);
        }

        #endregion

    }
}