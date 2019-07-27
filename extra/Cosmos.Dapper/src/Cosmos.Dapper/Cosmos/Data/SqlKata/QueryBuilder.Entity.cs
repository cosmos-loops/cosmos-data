using Dapper;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Dapper;
using Cosmos.Dapper.Core;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    public class EntityQueryBuilder : BaseQueryBuilder, IDisposable
    {
        private readonly bool _aloneMode;
        private readonly DapperOptions _options;
        private readonly IDapperConnector _connector;

        public EntityQueryBuilder(IDapperConnector connector, Compiler compiler, DapperOptions options, bool aloneMode = true)
            : base(connector.Connection, compiler)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        public EntityQueryBuilder(IDapperConnector connector, Compiler compiler, string table, DapperOptions options, bool aloneMode = true)
            : base(connector.Connection, compiler, table)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        private IDbTransaction Transaction => _connector.TransactionWrapper.CurrentTransaction;

        public T FindOne<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public T FindOne<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public async Task<T> FindOneAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public async Task<T> FindOneAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public int UniqueResultToInt(IDbTransaction transaction, CommandType? commandType = null)
            => UniqueResult<int>(transaction, commandType);

        public int UniqueResultToInt(CommandType? commandType = null)
            => UniqueResult<int>(commandType);

        public async Task<int> UniqueResultToIntAsync(IDbTransaction transaction, CommandType? commandType = null)
            => await UniqueResultAsync<int>(transaction, commandType);

        public async Task<int> UniqueResultToIntAsync(CommandType? commandType = null)
            => await UniqueResultAsync<int>(commandType);

        public long UniqueResultToLong(IDbTransaction transaction, CommandType? commandType = null)
            => UniqueResult<long>(transaction, commandType);

        public long UniqueResultToLong(CommandType? commandType = null)
            => UniqueResult<long>(commandType);

        public async Task<long> UniqueResultToLongAsync(IDbTransaction transaction, CommandType? commandType = null)
            => await UniqueResultAsync<long>(transaction, commandType);

        public async Task<long> UniqueResultToLongAsync(CommandType? commandType = null)
            => await UniqueResultAsync<long>(commandType);

        public T UniqueResult<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public T UniqueResult<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public async Task<T> UniqueResultAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public async Task<T> UniqueResultAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public IEnumerable<T> List<T>(IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        public IEnumerable<T> List<T>(bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        public async Task<IEnumerable<T>> ListAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public async Task<IEnumerable<T>> ListAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public bool SaveUpdate(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }

        public bool SaveUpdate(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        public async Task<bool> SaveUpdateAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }
        
        public async Task<bool> SaveUpdateAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        public bool SaveInsert(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }
        
        public bool SaveInsert(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        public async Task<bool> SaveInsertAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }
        
        public async Task<bool> SaveInsertAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        #region ForMysqlInserted

        public T SaveInsertForMysql<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public T SaveInsertForMysql<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public async Task<T> SaveInsertForMysqlAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public async Task<T> SaveInsertForMysqlAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        #endregion

        #region ForPostgresInserted

        public T SaveInsertForPostgreSql<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public T SaveInsertForPostgreSql<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public async Task<T> SaveInsertForPostgreSqlAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public async Task<T> SaveInsertForPostgreSqlAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        #endregion

        #region ForSqlServerInserted

        public T SaveInsertForSqlServer<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return _connection.QueryFirstOrDefault<T>(sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public T SaveInsertForSqlServer<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return _connection.QueryFirstOrDefault<T>(sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public async Task<T> SaveInsertForSqlServerAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public async Task<T> SaveInsertForSqlServerAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        internal protected string ReplaceSqlToGuid<T>(SqlResult result)
        {
            if (typeof(T) == typeof(Guid))
            {
                int index = result.Sql.IndexOf(" VALUES", StringComparison.Ordinal);
                if (index > -1)
                {
                    return result.Sql.Insert(index, " OUTPUT INSERTED.Id");
                }
            }

            return result.Sql;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            if (_aloneMode)
            {
                _connection = null;
            }

            _compiler = null;
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}