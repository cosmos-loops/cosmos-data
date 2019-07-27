﻿using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    public sealed class MySqlContextParams : IDapperContextParams
    {
        private readonly ISQLGenerator _generator;

        public MySqlContextParams(IDapperMappingConfig mappingConfig)
        {
            _generator = new SQLGenerator(mappingConfig);
        }

        public ISQLGenerator GetSqlGenerator() => _generator;

        public IDapperBulkInsertOperator GetBulkInsertOperator(IDapperConnector connector)
        {
            return new MySqlBulkInsertOperator(connector);
        }
    }
}