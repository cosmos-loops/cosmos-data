using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

using QueryOneType = Cosmos.Dapper.Core.DapperImplementor.QueryOneType;

namespace Cosmos.Dapper.Operations
{
    public partial interface IDapperEntityOperator
    {
        bool Delete<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(object predicate, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T GetOne<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault) where T : class;
        Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,  QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;
        T GetOne<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault) where T : class;
        Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;
        T First<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T First<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T Single<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T Single<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        int Count<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<int> CountAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        int Count<T>(object predicate, ISQLPredicate[] filters = null) where T : class;
        Task<int> CountAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
    }
}