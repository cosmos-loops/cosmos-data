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

        #region Get entity list by predicate

        public IEnumerable<T> GetList<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryListCommand<T>(connection, classMap, where, sort, transaction, Options.Timeout, buffered);
        }

        public Task<IEnumerable<T>> GetListAsync<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryListCommandAsync<T>(connection, classMap, where, sort, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Get entity list by expression

        public IEnumerable<T> GetList<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryListCommand<T>(connection, classMap, where, sort, transaction, Options.Timeout, buffered);
        }

        public Task<IEnumerable<T>> GetListAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryListCommandAsync<T>(connection, classMap, where, sort, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal helpers

        protected IEnumerable<T> ExecuteQueryListCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var sql = SQLGenerator.Select(classMap, predicate, sort, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            return connection.Query<T>(cmd);
        }

        protected async Task<IEnumerable<T>> ExecuteQueryListCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken) where T : class
        {
            var sql = SQLGenerator.Select(classMap, predicate, sort, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            return await connection.QueryAsync<T>(cmd).ConfigureAwait(false);
        }

        #endregion

    }
}