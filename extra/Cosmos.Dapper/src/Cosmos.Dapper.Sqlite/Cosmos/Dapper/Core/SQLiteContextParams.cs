using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    // ReSharper disable once InconsistentNaming
    public sealed class SQLiteContextParams : IDapperContextParams
    {
        private readonly ISQLGenerator _generator;

        public SQLiteContextParams(IDapperMappingConfig mappingConfig)
        {
            _generator = new SQLGenerator(mappingConfig);
        }

        public ISQLGenerator GetSqlGenerator() => _generator;

        public IDapperBulkInsertOperator GetBulkInsertOperator(IDapperConnector connector)
        {
            return new SQLiteBulkInsertOperator(connector);
        }
    }
}