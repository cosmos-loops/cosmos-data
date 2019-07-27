using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Data.SqlKata
{
    public partial class QueryBuilder
    {
        public IEnumerable<TReturn> Query<TReturn>(Type[] types, Func<object[], TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, types, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<TReturn>(Type[] types, Func<object[], TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, types, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(Func<T1, T2, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(Func<T1, T2, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TReturn>(Type[] types, Func<object[], TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, types, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TReturn>(Type[] types, Func<object[], TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, types, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(Func<T1, T2, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(Func<T1, T2, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }
    }
}