using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using QueryOneType = Cosmos.Dapper.Core.DapperImplementor.QueryOneType;

namespace Cosmos.Dapper.Core
{
    public partial class DapperConnector
    {
        public bool Delete<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, predicate, transaction, filters);

        public Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, predicate, transaction, filters, cancellationToken);

        public bool Delete<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, predicate, Transaction, filters);

        public Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.DeleteAsync(Connection, predicate, Transaction, filters, cancellationToken);

        public T GetOne<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filter = null, bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, Transaction, filter, buffered, type);

        public Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, Transaction, filters, type, cancellationToken);

        public T GetOne<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, Transaction, filters, buffered, type);

        public Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, Transaction, filters, type, cancellationToken);

        public T First<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.First);

        public Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, transaction, filters, QueryOneType.First, cancellationToken);

        public T First<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.First);

        public Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, Transaction, filters, QueryOneType.First, cancellationToken);

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.FirstOrDefault);

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, transaction, filters, QueryOneType.FirstOrDefault, cancellationToken);

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.FirstOrDefault);

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, Transaction, filters, QueryOneType.FirstOrDefault, cancellationToken);
       
        public T Single<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.Single);

        public Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, transaction, filters, QueryOneType.Single, cancellationToken);

        public T Single<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.Single);

        public Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, Transaction, filters, QueryOneType.Single, cancellationToken);

        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.SingleOrDefault);

        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, transaction, filters, QueryOneType.SingleOrDefault, cancellationToken);

        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.SingleOrDefault);

        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync(Connection, predicate, sortSet, Transaction, filters, QueryOneType.SingleOrDefault, cancellationToken);

        public IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetList(Connection, predicate, sort, transaction, filters, buffered);

        public Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetListAsync(Connection, predicate, sort, transaction, filters, cancellationToken);

        public IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetList(Connection, predicate, sort, Transaction, filters, buffered);

        public Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetListAsync(Connection, predicate, sort, Transaction, filters, cancellationToken);

        public IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetPage(Connection, predicate, sort, pageNumber, pageSize, transaction, filters, buffered);

        public Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetPageAsync(Connection, predicate, sort, pageNumber, pageSize, transaction, filters, cancellationToken);

        public IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetPage(Connection, predicate, sort, pageNumber, pageSize, Transaction, filters, buffered);

        public Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetPageAsync(Connection, predicate, sort, pageNumber, pageSize, Transaction, filters, cancellationToken);

        public IEnumerable<T> GetSet<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetSet(Connection, predicate, sort, limitFrom, limitTo, transaction, filters, buffered);

        public Task<IEnumerable<T>> GetSetAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetSetAsync(Connection, predicate, sort, limitFrom, limitTo, transaction, filters, cancellationToken);

        public IEnumerable<T> GetSet<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetSet(Connection, predicate, sort, limitFrom, limitTo, Transaction, filters, buffered);

        public Task<IEnumerable<T>> GetSetAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetSetAsync(Connection, predicate, sort, limitFrom, limitTo, Transaction, filters, cancellationToken);

        public int Count<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Count(Connection, predicate, transaction, filters);

        public Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.CountAsync(Connection, predicate, transaction, filters, cancellationToken);

        public int Count<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null) where T : class
            => _proxy.Count(Connection, predicate, Transaction, filters);

        public Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.CountAsync(Connection, predicate, Transaction, filters, cancellationToken);

    }
}