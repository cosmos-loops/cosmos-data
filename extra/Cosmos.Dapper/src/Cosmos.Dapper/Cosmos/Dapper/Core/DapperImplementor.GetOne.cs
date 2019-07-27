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

        #region Get one entity by predicate

        public T GetOne<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryOneCommand<T>(connection, classMap, where, sort, transaction, Options.Timeout, buffered, type);
        }

        public Task<T> GetOneAsync<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryOneCommandAsync<T>(connection, classMap, where, sort, transaction, Options.Timeout, cancellationToken, type);
        }

        #endregion

        #region Get one entity by expression

        public T GetOne<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryOneCommand<T>(connection, classMap, where, sort, transaction, Options.Timeout, buffered, type);
        }

        public Task<T> GetOneAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryOneCommandAsync<T>(connection, classMap, where, sort, transaction, Options.Timeout, cancellationToken, type);
        }

        #endregion

        #region internal helpers

        protected T ExecuteQueryOneCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort,
            IDbTransaction transaction, int? commandTimeout, bool buffered, QueryOneType type) where T : class
        {
            var sql = SQLGenerator.Select(classMap, predicate, sort, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            switch (type)
            {
                case QueryOneType.First:
                    return connection.QueryFirst<T>(cmd);
                case QueryOneType.FirstOrDefault:
                    return connection.QueryFirstOrDefault<T>(cmd);
                case QueryOneType.Single:
                    return connection.QuerySingle<T>(cmd);
                case QueryOneType.SingleOrDefault:
                    return connection.QuerySingleOrDefault<T>(cmd);
                default:
                    throw new InvalidOperationException("Invalid operation type for dapper implementor.");
            }
        }

        protected async Task<T> ExecuteQueryOneCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken, QueryOneType type) where T : class
        {
            var sql = SQLGenerator.Select(classMap, predicate, sort, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            switch (type)
            {
                case QueryOneType.First:
                    return await connection.QueryFirstAsync<T>(cmd);
                case QueryOneType.FirstOrDefault:
                    return await connection.QueryFirstOrDefaultAsync<T>(cmd);
                case QueryOneType.Single:
                    return await connection.QuerySingleAsync<T>(cmd);
                case QueryOneType.SingleOrDefault:
                    return await connection.QuerySingleOrDefaultAsync<T>(cmd);
                default:
                    throw new InvalidOperationException("Invalid operation type for dapper implementor.");
            }
        }

        public enum QueryOneType
        {
            First,
            FirstOrDefault,
            Single,
            SingleOrDefault
        }

        #endregion

    }
}