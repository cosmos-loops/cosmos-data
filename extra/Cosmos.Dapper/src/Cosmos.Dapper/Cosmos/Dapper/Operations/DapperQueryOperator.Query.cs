using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {

        #region Query

        public IEnumerable<object> Query(string sql, object param = null, bool buffered = true, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, param, Transaction, buffered, Options.Timeout, commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query<T>(sql, param, Transaction, buffered, Options.Timeout, commandType);
        }

        public IEnumerable<object> Query(Type type, string sql, object param = null, bool buffered = true, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(type, sql, param, Transaction, buffered, Options.Timeout, commandType);
        }

        public IEnumerable<T> Query<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query<T>(InjectTransaction(command));
        }

        #endregion

        #region multi-query

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

         public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, types, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        #endregion

        #region crosscut multi-query

        public IEnumerable<T1> Query<T1, T2>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<T1> Query<T1, T2, T3>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<T1> Query<T1, T2, T3, T4>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<T1> Query<T1, T2, T3, T4, T5>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4, T5>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4, T5, T6>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4, T5, T6, T7>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        #endregion

    }
}