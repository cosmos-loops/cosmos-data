using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public interface IDapperCommandOperator
    {
        Task<object> ExecuteScalarAsync(string sql, object param = null, CommandType? commandType = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType? commandType = null);
        Task<object> ExecuteScalarAsync(CommandDefinition command);
        Task<T> ExecuteScalarAsync<T>(CommandDefinition command);
        Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null);
        Task<int> ExecuteAsync(CommandDefinition command);
        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, CommandType? commandType = null);
        Task<IDataReader> ExecuteReaderAsync(CommandDefinition command);
        int Execute(string sql, object param = null, CommandType? commandType = null);
        int Execute(CommandDefinition command);
        object ExecuteScalar(string sql, object param = null, CommandType? commandType = null);
        T ExecuteScalar<T>(string sql, object param = null, CommandType? commandType = null);
        object ExecuteScalar(CommandDefinition command);
        T ExecuteScalar<T>(CommandDefinition command);
        IDataReader ExecuteReader(string sql, object param = null, CommandType? commandType = null);
        IDataReader ExecuteReader(CommandDefinition command);
        IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior);
    }
}