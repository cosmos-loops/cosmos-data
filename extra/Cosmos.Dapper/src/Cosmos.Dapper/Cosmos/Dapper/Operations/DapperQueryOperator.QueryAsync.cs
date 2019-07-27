using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {

        #region Query

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, param, Transaction,  Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T>(sql, param, Transaction,  Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<object>> QueryAsync(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(type, sql, param, Transaction,  Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<object>> QueryAsync(Type type, CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(type, InjectTransaction(command)).ConfigureAwait(false);
        }

        #endregion

        #region multi-query

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, types, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        #endregion

        #region crosscut multi-query

        public async Task<IEnumerable<T1>> QueryAsync<T1, T2>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4, T5>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4, T5, T6>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4, T5, T6, T7>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        #endregion

    }
}