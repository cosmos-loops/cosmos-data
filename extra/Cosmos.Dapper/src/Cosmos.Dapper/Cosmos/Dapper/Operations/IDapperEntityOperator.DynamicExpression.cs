using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

using QueryOneType = Cosmos.Dapper.Core.DapperImplementor.QueryOneType;

namespace Cosmos.Dapper.Operations
{
    public partial interface IDapperEntityOperator
    {
        bool Delete<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        bool Delete<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T GetOne<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault) where T : class;
        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,  QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;
        T GetOne<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault) where T : class;
        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;
        T First<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T First<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T Single<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T Single<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetSet<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetSet<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        int Count<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        int Count<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
    }
}