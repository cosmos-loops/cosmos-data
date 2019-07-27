using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core
{
    public interface IDapperContextParams
    {
        ISQLGenerator GetSqlGenerator();

        IDapperBulkInsertOperator GetBulkInsertOperator(IDapperConnector connector);
    }
}