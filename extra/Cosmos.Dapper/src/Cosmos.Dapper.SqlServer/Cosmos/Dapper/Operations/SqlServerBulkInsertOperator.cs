using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Dapper.Mapper;

namespace Cosmos.Dapper.Operations
{
    public class SqlServerBulkInsertOperator : DapperBulkInsertOperator
    {
        public SqlServerBulkInsertOperator(IDapperConnector connector) : base(connector) { }

        public override void Process<T>(IList<T> dataSet)
        {
            if (dataSet == null || !dataSet.Any())
                return;

            var options = Options;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var bulkCopy = new SqlBulkCopy(GetConnection<SqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                UpdateBulkColumnMapping(bulkCopy, classMap);
                bulkCopy.BatchSize = options.BatchSize;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                bulkCopy.WriteToServer(dt);
                bulkCopy.Close();
            }
        }

        public override async Task ProcessAsync<T>(IList<T> dataSet)
        {
            if (dataSet == null || !dataSet.Any())
                return;

            var options = Options;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var bulkCopy = new SqlBulkCopy(GetConnection<SqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                UpdateBulkColumnMapping(bulkCopy, classMap);
                bulkCopy.BatchSize = options.BatchSize;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                await bulkCopy.WriteToServerAsync(dt);
                bulkCopy.Close();
            }
        }

        private static void UpdateBulkColumnMapping(SqlBulkCopy bulkCopy, IClassMap classMap)
        {
            foreach (var propertyMap in classMap.PropertyMaps)
                bulkCopy.ColumnMappings.Add(propertyMap.ColumnName, propertyMap.ColumnName);
        }
    }
}