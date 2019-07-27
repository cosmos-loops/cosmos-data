using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.SqlBulkCopy;

namespace Cosmos.Dapper.Operations
{
    public class MySqlBulkInsertOperator : DapperBulkInsertOperator
    {
        public MySqlBulkInsertOperator(IDapperConnector connector) : base(connector) { }

        public override void Process<T>(IList<T> dataSet)
        {
            if (dataSet == null || !dataSet.Any())
                return;

            var options = Options;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var bulkCopy = new MySqlBulkCopy(GetConnection<MySqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                bulkCopy.SecureFilePriv = options.SecureFilePriv ?? AppDomain.CurrentDomain.BaseDirectory;
                bulkCopy.ClearTempCsvAfterWriting = true;
                bulkCopy.WriteToServer(dt);
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

            using (var bulkCopy = new MySqlBulkCopy(GetConnection<MySqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                bulkCopy.SecureFilePriv = options.SecureFilePriv ?? AppDomain.CurrentDomain.BaseDirectory;
                bulkCopy.ClearTempCsvAfterWriting = true;
                await bulkCopy.WriteToServerAsync(dt);
            }
        }
    }
}