using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Cosmos.Data.SqlKata
{
    public static class QueryBuilderExtensions
    {
        public static int Execute(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.Execute(transaction, commandType);
        }

        public static Task<int> ExecuteAsync(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.ExecuteAsync(transaction, commandType);
        }

        public static IEnumerable<TReturn> Query<TReturn>(this QueryBuilder query, Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(types, map, transaction, buffered, splitOn, commandType);
        }

        public static IEnumerable<TReturn> Query<T1, T2, TReturn>(
            this QueryBuilder query, Func<T1, T2, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(map, transaction, buffered, splitOn, commandType);
        }

        public static IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(map, transaction, buffered, splitOn, commandType);
        }

        public static IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(map, transaction, buffered, splitOn, commandType);
        }

        public static IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, T5, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(map, transaction, buffered, splitOn, commandType);
        }
        
        public static IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, T5, T6, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(map, transaction, buffered, splitOn, commandType);
        }
        
        public static IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.Query(map, transaction, buffered, splitOn, commandType);
        }
        
        public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this QueryBuilder query,
            Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(types, map, transaction, buffered, splitOn, commandType);
        }
        
        public static Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(
            this QueryBuilder query, Func<T1, T2, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(map, transaction, buffered, splitOn, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(map, transaction, buffered, splitOn, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(map, transaction, buffered, splitOn, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, T5, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(map, transaction, buffered, splitOn, commandType);
        }
        
        public static Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, T5, T6, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(map, transaction, buffered, splitOn, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(
            this QueryBuilder query, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            return query.QueryAsync(map, transaction, buffered, splitOn, commandType);
        }

        public static IEnumerable<dynamic> Query(this QueryBuilder query, IDbTransaction transaction = null, bool buffered = true,
            CommandType? commandType = null)
        {
            return query.Query(transaction, buffered, commandType);
        }

        public static IEnumerable<T> Query<T>(this QueryBuilder query, IDbTransaction transaction = null, bool buffered = true,
            CommandType? commandType = null)
        {
            return query.Query<T>(transaction, buffered, commandType);
        }

        public static IEnumerable<object> Query(this QueryBuilder query, Type type, string sql, IDbTransaction transaction = null, bool buffered = true,
            CommandType? commandType = null)
        {
            return query.Query(type, transaction, buffered, commandType);
        }

        public static Task<IEnumerable<dynamic>> QueryAsync(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryAsync(transaction, commandType);
        }

        public static Task<IEnumerable<object>> QueryAsync(this QueryBuilder query, Type type, IDbTransaction transaction = null,
            CommandType? commandType = null)
        {
            return query.QueryAsync(type, transaction, commandType);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryAsync<T>(transaction, commandType);
        }

        public static dynamic QueryFirst(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirst(transaction, commandType);
        }

        public static object QueryFirst(this QueryBuilder query, Type type, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirst(transaction, commandType);
        }

        public static T QueryFirst<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirst<T>(transaction, commandType);
        }

        public static Task<object> QueryFirstAsync(this QueryBuilder query, Type type, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirstAsync(type, transaction, commandType);
        }

        public static Task<T> QueryFirstAsync<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirstAsync<T>(transaction, commandType);
        }

        public static T QueryFirstOrDefault<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirstOrDefault<T>(transaction, commandType);
        }

        public static object QueryFirstOrDefault(this QueryBuilder query, Type type, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirstOrDefault(type, transaction, commandType);
        }

        public static dynamic QueryFirstOrDefault(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirstOrDefault(transaction, commandType);
        }

        public static Task<object> QueryFirstOrDefaultAsync(this QueryBuilder query, Type type, IDbTransaction transaction = null,
            CommandType? commandType = null)
        {
            return query.QueryFirstOrDefaultAsync(type, transaction, commandType);
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryFirstOrDefaultAsync<T>(transaction, commandType);
        }

        public static GridReader QueryMultiple(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryMultiple(transaction, commandType);
        }

        public static Task<GridReader> QueryMultipleAsync(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QueryMultipleAsync(transaction, commandType);
        }

        public static dynamic QuerySingle(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingle(transaction, commandType);
        }

        public static object QuerySingle(this QueryBuilder query, Type type, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingle(type, transaction, commandType);
        }

        public static T QuerySingle<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingle<T>(transaction, commandType);
        }

        public static Task<T> QuerySingleAsync<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingleAsync<T>(transaction, commandType);
        }

        public static Task<object> QuerySingleAsync(this QueryBuilder query, Type type, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingleAsync(type, transaction, commandType);
        }

        public static T QuerySingleOrDefault<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingleOrDefault<T>(transaction, commandType);
        }

        public static Task<dynamic> QuerySingleOrDefaultAsync<T>(this QueryBuilder query, Type type, IDbTransaction transaction = null,
            CommandType? commandType = null)
        {
            return query.QuerySingleOrDefaultAsync(type, transaction, commandType);
        }

        public static Task<T> QuerySingleOrDefaultAsync<T>(this QueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return query.QuerySingleOrDefaultAsync<T>(transaction, commandType);
        }
    }
}