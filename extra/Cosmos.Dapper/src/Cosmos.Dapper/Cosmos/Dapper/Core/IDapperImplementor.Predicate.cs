using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Dapper;

// ReSharper disable InconsistentNaming

namespace Cosmos.Dapper.Core
{
    public partial interface IDapperImplementor
    {
        bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<bool> DeleteAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        T GetOne<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true, DapperImplementor.QueryOneType type = DapperImplementor.QueryOneType.FirstOrDefault) where T : class;
        Task<T> GetOneAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, DapperImplementor.QueryOneType type = DapperImplementor.QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, int pageNumber, int pageSize,IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IEnumerable<T> GetSet<T>(IDbConnection connection, object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)where T : class; 
        Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;
        Task<int> CountAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
        IMultipleResultReader GetMultiple(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction);
        Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction, CancellationToken cancellationToken = default);
    }
}