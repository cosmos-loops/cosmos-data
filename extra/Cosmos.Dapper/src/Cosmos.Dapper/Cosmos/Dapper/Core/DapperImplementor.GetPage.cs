using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {

        #region Get Page by predicate

        public IEnumerable<T> GetPage<T>(
            IDbConnection connection,
            object predicate, SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction, 
            ISQLPredicate[] filters = null,
            bool buffered = true)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join( filters);
            return ExecuteQueryPageCommand<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, buffered);
        }

        public Task<IEnumerable<T>> GetPageAsync<T>(
            IDbConnection connection,
            object predicate, SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction, 
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join( filters);
            return ExecuteQueryPageCommandAsync<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Get page by expression

        public IEnumerable<T> GetPage<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join( filters);
            return ExecuteQueryPageCommand<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, buffered);
        }

        public Task<IEnumerable<T>> GetPageAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join( filters);
            return ExecuteQueryPageCommandAsync<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal helpers

        protected IEnumerable<T> ExecuteQueryPageCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int page, int resultsPerPage,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var sql = SQLGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            return connection.Query<T>(cmd);
        }

        protected async Task<IEnumerable<T>> ExecuteQueryPageCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int page,
            int resultsPerPage, IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken) where T : class
        {
            var sql = SQLGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            return await connection.QueryAsync<T>(cmd).ConfigureAwait(false);
        }

        #endregion

    }
}