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
        public bool Delete<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete<T>(Connection, predicate, transaction, filters);

        public Task<bool> DeleteAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.DeleteAsync<T>(Connection, predicate, transaction, filters, cancellationToken);

        public bool Delete<T>(object predicate, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete<T>(Connection, predicate, Transaction, filters);

        public Task<bool> DeleteAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync<T>(Connection, predicate, Transaction, filters, cancellationToken);

        public T GetOne<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, type);

        public Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, type, cancellationToken);

        public T GetOne<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, type);

        public Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, type, cancellationToken);

        public T First<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.First);

        public Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.First, cancellationToken);

        public T First<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.First);

        public Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.First, cancellationToken);

        public T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.FirstOrDefault);

        public Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.FirstOrDefault, cancellationToken);

        public T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.FirstOrDefault);

        public Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.FirstOrDefault, cancellationToken);

        public T Single<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.Single);

        public Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.Single, cancellationToken);

        public T Single<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.Single);

        public Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.Single, cancellationToken);

        public T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.SingleOrDefault);

        public Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.SingleOrDefault, cancellationToken);

        public T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.SingleOrDefault);

        public Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.SingleOrDefault, cancellationToken);

        public IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetList<T>(Connection, predicate, sort, transaction, filters, buffered);

        public Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetListAsync<T>(Connection, predicate, sort, transaction, filters, cancellationToken);

        public IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetList<T>(Connection, predicate, sort, Transaction, filters, buffered);

        public Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetListAsync<T>(Connection, predicate, sort, Transaction, filters, cancellationToken);

        public IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetPage<T>(Connection, predicate, sort, pageNumber, pageSize, transaction, filters, buffered);

        public Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetPageAsync<T>(Connection, predicate, sort, pageNumber, pageSize, transaction, filters, cancellationToken);

        public IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetPage<T>(Connection, predicate, sort, pageNumber, pageSize, Transaction, filters, buffered);

        public Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetPageAsync<T>(Connection, predicate, sort, pageNumber, pageSize, Transaction, filters, cancellationToken);

        public IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetSet<T>(Connection, predicate, sort, limitFrom, limitTo, transaction, filters, buffered);

        public Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetSetAsync<T>(Connection, predicate, sort, limitFrom, limitTo, transaction, filters, cancellationToken);

        public IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetSet<T>(Connection, predicate, sort, limitFrom, limitTo, Transaction, filters, buffered);

        public Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetSetAsync<T>(Connection, predicate, sort, limitFrom, limitTo, Transaction, filters, cancellationToken);

        public int Count<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Count<T>(Connection, predicate, transaction, filters);

        public Task<int> CountAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.CountAsync<T>(Connection, predicate, transaction, filters, cancellationToken);

        public int Count<T>(object predicate, ISQLPredicate[] filters = null) where T : class
            => _proxy.Count<T>(Connection, predicate, Transaction, filters);

        public Task<int> CountAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.CountAsync<T>(Connection, predicate, Transaction, filters, cancellationToken);

    }
}