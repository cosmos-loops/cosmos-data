using System.Data;
using System.Threading.Tasks;

namespace Cosmos.Data.SqlKata
{
    public static class EntityQueryExtensions
    {
        public static T SaveInsert<T>(this EntityQueryBuilder query,
            IDbTransaction transaction = null, CommandType? commandType = null) where T : struct
            => query.SaveInsertForMysql<T>(transaction, commandType);

        public static async Task<T> SaveInsertAsync<T>(this EntityQueryBuilder query,
            IDbTransaction transaction = null, CommandType? commandType = null) where T : struct
            => await query.SaveInsertForMysqlAsync<T>(transaction, commandType);
    }
}