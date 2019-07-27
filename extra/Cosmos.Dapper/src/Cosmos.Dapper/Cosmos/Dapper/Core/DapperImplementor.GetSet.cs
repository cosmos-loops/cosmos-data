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

        #region Get set by predicate

        public IEnumerable<T> GetSet<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQuerySetCommand<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, buffered);
        }

        public Task<IEnumerable<T>> GetSetAsync<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQuerySetCommandAsync<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Get set by expression

        public IEnumerable<T> GetSet<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool buffered = true) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQuerySetCommand<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, buffered);
        }

        public Task<IEnumerable<T>> GetSetAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQuerySetCommandAsync<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal helpers

        protected IEnumerable<T> ExecuteQuerySetCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int firstResult, int maxResults,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var sql = SQLGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            return connection.Query<T>(cmd);
        }

        protected async Task<IEnumerable<T>> ExecuteQuerySetCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int firstResult,
            int maxResults, IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken) where T : class
        {
            var sql = SQLGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            return await connection.QueryAsync<T>(cmd).ConfigureAwait(false);
        }

        #endregion

    }
}