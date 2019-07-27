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

        #region Delete by single entity

        public bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
        {
            var classMap = GetClassMap<T>();
            var predicate = GetKeyPredicate(classMap, entity).Join(filters);
            return ExecuteDeleteCommand(connection, classMap, predicate, transaction, Options.Timeout);
        }

        public async Task<bool> DeleteAsync<T>(IDbConnection connection, T entity,
            IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var predicate = GetKeyPredicate(classMap, entity).Join(filters);
            return await ExecuteDeleteCommandAsync(connection, classMap, predicate, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Delete by multi entities

        public bool Delete<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
        {
            var classMap = GetClassMap<T>();
            var predicate = GetKeyPredicate(classMap, entities).Join(filters);
            return ExecuteDeleteCommand(connection, classMap, predicate, transaction, Options.Timeout);
        }

        public async Task<bool> DeleteAsync<T>(IDbConnection connection, IEnumerable<T> entities,
            IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var predicate = GetKeyPredicate(classMap, entities).Join(filters);
            return await ExecuteDeleteCommandAsync(connection, classMap, predicate, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Delete by predicate

        public bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
        {
            var classMap = GetClassMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteDeleteCommand(connection, classMap, wherePredicate, transaction, Options.Timeout);
        }

        public async Task<bool> DeleteAsync<T>(IDbConnection connection, object predicate,
            IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate).Join(filters);
            return await ExecuteDeleteCommandAsync(connection, classMap, wherePredicate, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Delete by expression

        public bool Delete<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var wherePredicate = ConvertToPredicate(predicate).Join(filters);
            return ExecuteDeleteCommand(connection, classMap, wherePredicate, transaction, Options.Timeout);
        }

        public async Task<bool> DeleteAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var wherePredicate = ConvertToPredicate(predicate).Join(filters);
            return await ExecuteDeleteCommandAsync(connection, classMap, wherePredicate, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal helpers

        private bool ExecuteDeleteCommand(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            var cmd = GetDeleteSql(classMap, predicate).ToSQLCommand(transaction, commandTimeout);
            return connection.Execute(cmd) > 0;
        }

        private async Task<bool> ExecuteDeleteCommandAsync(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken = default)
        {
            var cmd = GetDeleteSql(classMap, predicate).ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            return await connection.ExecuteAsync(cmd) > 0;
        }

        private SQLConvertResult GetDeleteSql(IClassMap classMap, ISQLPredicate predicate)
            => SQLGenerator.Delete(classMap, predicate, new Dictionary<string, object>());

        #endregion

    }
}