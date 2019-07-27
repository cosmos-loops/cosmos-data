using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cosmos.Data.SqlKata
{
    public static class EntityQueryBuilderExtensions
    {
        public static T FindOne<T>(this EntityQueryBuilder query, IDbTransaction transaction, CommandType? commandType = null)
            => query.FindOne<T>(transaction, commandType);

        public static async Task<T> FindOneAsync<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.FindOneAsync<T>(transaction, commandType);

        public static int UniqueResultToInt(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => UniqueResult<int>(query, transaction, commandType);

        public static async Task<int> UniqueResultToIntAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await UniqueResultAsync<int>(query, transaction, commandType);

        public static long UniqueResultToLong(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => UniqueResult<long>(query, transaction, commandType);

        public static async Task<long> UniqueResultToLongAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await UniqueResultAsync<long>(query, transaction, commandType);

        public static T UniqueResult<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => query.UniqueResult<T>(transaction, commandType);

        public static async Task<T> UniqueResultAsync<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.UniqueResultAsync<T>(transaction, commandType);

        public static IEnumerable<T> List<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, bool buffered = true, CommandType? commandType = null)
            => query.List<T>(transaction, buffered, commandType);

        public static async Task<IEnumerable<T>> ListAsync<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.ListAsync<T>(transaction, commandType);

        public static bool Update(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => query.SaveUpdate(transaction, commandType);

        public static async Task<bool> UpdateAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.SaveUpdateAsync(transaction, commandType);

        public static bool Insert(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => query.SaveInsert(transaction, commandType);

        public static async Task<bool> InsertAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.SaveInsertAsync(transaction, commandType);
    }
}