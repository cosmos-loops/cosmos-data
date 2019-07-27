using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.Contextual
{
    public interface IWithSQLGenerator
    {
        ISQLGenerator SqlGenerator { get; set; }
    }
}