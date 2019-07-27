using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Client.SqlBulkCopy;

namespace Cosmos.Dapper.Operations
{
    public class OracleBulkInsertOperator : DapperBulkInsertOperator
    {
        public OracleBulkInsertOperator(IDapperConnector connector) : base(connector) { }

        public override void Process<T>(IList<T> dataSet)
        {
            if (dataSet == null || !dataSet.Any()) return;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var bulkCopy = new OracleBulkCopy(GetConnection<OracleConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = Options.BatchSize;
                bulkCopy.BulkCopyTimeout = Options.Timeout;
                bulkCopy.WriteToServer(dt);
                bulkCopy.Close();
            }
        }

        public override Task ProcessAsync<T>(IList<T> dataSet)
        {
            Process(dataSet);
            return Task.CompletedTask;
        }
    }
}