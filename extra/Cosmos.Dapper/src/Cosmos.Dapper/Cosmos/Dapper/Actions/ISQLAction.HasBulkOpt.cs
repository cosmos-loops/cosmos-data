using Cosmos.Dapper.Core;

namespace Cosmos.Dapper.Actions
{
    public interface IHasBulkOpt
    {
        IDapperContextParams ContextParams { get; set; }
    }
}