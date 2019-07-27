using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {

        #region Get multiple result by predicate

        public IMultipleResultReader GetMultiple(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction)
        {
            if (SQLGenerator.SupportsMultipleStatements())
                return GetMultipleByBatch(connection, predicate, transaction, Options.Timeout);
            return GetMultipleBySequence(connection, predicate, transaction, Options.Timeout);
        }

        public async Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction,
            CancellationToken cancellationToken = default)
        {
            if (SQLGenerator.SupportsMultipleStatements())
                return await GetMultipleByBatchAsync(connection, predicate, transaction, Options.Timeout, cancellationToken);
            return await GetMultipleBySequenceAsync(connection, predicate, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal implementations

        protected GridReaderResultReader GetMultipleByBatch(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            var sql = new SQLConvertResult();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var tmp = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, sql.WritableParameters);
                sql.AppendSqlLine(tmp.Sql);
                sql.AppendSqlLine(SQLGenerator.Configuration.Dialect.BatchSeperator);
            }

            var grid = connection.QueryMultiple(sql.ToSQLCommand(transaction, commandTimeout));
            return new GridReaderResultReader(grid);
        }

        protected async Task<GridReaderResultReader> GetMultipleByBatchAsync(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken)
        {
            var sql = new SQLConvertResult();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var tmp = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, sql.WritableParameters);
                sql.AppendSqlLine(tmp.Sql);
                sql.AppendSqlLine(SQLGenerator.Configuration.Dialect.BatchSeperator);
            }

            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            var grid = await connection.QueryMultipleAsync(cmd).ConfigureAwait(false);
            return new GridReaderResultReader(grid);
        }

        protected SequenceReaderResultReader GetMultipleBySequence(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            var items = new List<SqlMapper.GridReader>();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var sql = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, new Dictionary<string, object>());
                var queryRet = connection.QueryMultiple(sql.ToSQLCommand(transaction, commandTimeout));
                items.Add(queryRet);
            }

            return new SequenceReaderResultReader(items);
        }

        protected async Task<SequenceReaderResultReader> GetMultipleBySequenceAsync(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken)
        {
            var items = new List<SqlMapper.GridReader>();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var sql = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, new Dictionary<string, object>());
                var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
                var queryRet = await connection.QueryMultipleAsync(cmd).ConfigureAwait(false);
                items.Add(queryRet);
            }

            return new SequenceReaderResultReader(items);
        }

        #endregion

        #region internal helpers

        private ISQLPredicate GetMultiplePredicate(IClassMap classMap, SQLMultiplePredicate.SQLMultiplePredicateItem item)
        {
            var itemPredicate = item.Value as ISQLPredicate;

            if (itemPredicate == null && item.Value != null)
                itemPredicate = GetPredicate(classMap, item.Value);

            return itemPredicate;
        }

        #endregion

    }
}