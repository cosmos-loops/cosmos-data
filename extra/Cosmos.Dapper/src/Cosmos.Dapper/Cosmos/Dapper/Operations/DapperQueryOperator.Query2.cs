using System;
using System.Data;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {

        #region  Dapper Proxy Members

        public object QueryFirst(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst(sql, param, Transaction, Options.Timeout, commandType);
        }

        public object QueryFirstOrDefault(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault(sql, param, Transaction, Options.Timeout, commandType);
        }
        
        public object QuerySingle(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle(sql, param, Transaction, Options.Timeout, commandType);
        }

        public object QuerySingleOrDefault(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault(sql, param, Transaction, Options.Timeout, commandType);
        }

        public T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        public T QuerySingle<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        public object QueryFirst(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        public object QueryFirstOrDefault(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        public object QuerySingle(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        public object QuerySingleOrDefault(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        public T QueryFirst<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst<T>(InjectTransaction(command));
        }

        public T QueryFirstOrDefault<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault<T>(InjectTransaction(command));
        }

        public T QuerySingle<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle<T>(InjectTransaction(command));
        }

        public T QuerySingleOrDefault<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault<T>(InjectTransaction(command));
        }

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryMultiple(sql, param, Transaction, Options.Timeout, commandType);
        }

        public SqlMapper.GridReader QueryMultiple(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryMultiple(InjectTransaction(command));
        }

        #endregion

    }
}