using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Actions
{
    public interface IHasDataFilter
    {
        ISQLPredicate[] Filters { get; set; }
    }
}