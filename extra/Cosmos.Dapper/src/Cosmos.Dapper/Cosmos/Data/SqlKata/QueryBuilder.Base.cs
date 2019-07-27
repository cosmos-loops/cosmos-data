using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Dapper;
using Cosmos.Dapper.Core;
using SqlKata.Compilers;
using static Dapper.SqlMapper;

namespace Cosmos.Data.SqlKata
{
    public partial class QueryBuilder : BaseQueryBuilder, IDisposable
    {
        private readonly bool _aloneMode;
        private readonly DapperOptions _options;
        private readonly IDapperConnector _connector;

        public QueryBuilder(IDapperConnector connector, Compiler compiler, DapperOptions options, bool aloneMode = true)
            : base(connector.Connection, compiler)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        public QueryBuilder(IDapperConnector connector, Compiler compiler, DapperOptions options, string table, bool aloneMode = true)
            : base(connector.Connection, compiler, table)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        private IDbTransaction Transaction => _connector.TransactionWrapper.CurrentTransaction;

        public int Execute(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public int Execute(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<int> ExecuteAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<int> ExecuteAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.ExecuteAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public IEnumerable<dynamic> Query(IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        public IEnumerable<dynamic> Query(bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        public IEnumerable<T> Query<T>(IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        public IEnumerable<T> Query<T>(bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        public IEnumerable<object> Query(Type type, IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(type, result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        public IEnumerable<object> Query(Type type, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(type, result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        public Task<IEnumerable<dynamic>> QueryAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<IEnumerable<dynamic>> QueryAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<IEnumerable<object>> QueryAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<IEnumerable<object>> QueryAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public dynamic QueryFirst(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public dynamic QueryFirst(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public object QueryFirst(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public object QueryFirst(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public T QueryFirst<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public T QueryFirst<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<object> QueryFirstAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<object> QueryFirstAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<T> QueryFirstAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<T> QueryFirstAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public T QueryFirstOrDefault<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public T QueryFirstOrDefault<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public object QueryFirstOrDefault(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public object QueryFirstOrDefault(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public dynamic QueryFirstOrDefault(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public dynamic QueryFirstOrDefault(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<object> QueryFirstOrDefaultAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<object> QueryFirstOrDefaultAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public GridReader QueryMultiple(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultiple(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public GridReader QueryMultiple(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultiple(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<GridReader> QueryMultipleAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultipleAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<GridReader> QueryMultipleAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultipleAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public dynamic QuerySingle(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public dynamic QuerySingle(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public object QuerySingle(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public object QuerySingle(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public T QuerySingle<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public T QuerySingle<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<T> QuerySingleAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<T> QuerySingleAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<object> QuerySingleAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public Task<object> QuerySingleAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public T QuerySingleOrDefault<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        public T QuerySingleOrDefault<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<dynamic> QuerySingleOrDefaultAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public Task<dynamic> QuerySingleOrDefaultAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }
        
        public Task<T> QuerySingleOrDefaultAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        #region Dispose

        public void Dispose()
        {
            if (_aloneMode)
            {
                _connection?.Dispose();
                _connection = null;
            }

            _compiler = null;
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}