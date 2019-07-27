using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Data.Transaction;
using Dapper;
using Dapper.Mapper;

namespace Cosmos.Dapper.Operations
{
    public abstract class DapperBulkInsertOperator : IDapperBulkInsertOperator
    {
        private readonly IDapperConnector _connector;
        // ReSharper disable once NotAccessedField.Local
        private readonly ITransactionWrapper _transactionPointer;
        private readonly IDapperMappingConfig _mappingConfig;

        protected DapperBulkInsertOperator(IDapperConnector connector)
        {
            _connector = connector;
            _transactionPointer = connector.TransactionWrapper;
            _mappingConfig = DapperConfigAccessor.Cache(_connector.Connection.ConnectionString);
        }

        public abstract void Process<T>(IList<T> dataSet) where T : class;

        public abstract Task ProcessAsync<T>(IList<T> dataSet) where T : class;

        protected IClassMap<T> GetMap<T>() where T : class
        {
            return _mappingConfig.GetMap<T>();
        }

        protected string GetTableName<T>() where T : class
        {
            var classMap = GetMap<T>();
            return _mappingConfig.Dialect.GetTableName(classMap.SchemaName, classMap.TableName, string.Empty);
        }

        protected string QuoteString(string value)
        {
            return _mappingConfig.Dialect.QuoteString(value);
        }

        protected TConnection GetConnection<TConnection>() where TConnection : class, IDbConnection
        {
            return _connector.Connection as TConnection;
        }

        protected DapperOptions Options => _mappingConfig.Options;

        protected static class DataTableBuilder
        {
            public static DataTable Build<T>(IClassMap<T> classMap, IList<T> dataSet, string tableName) where T : class
            {
                var propertyMaps = classMap.PropertyMaps;

                var table = new DataTable();
                table.BeginLoadData();
                table.TableName = tableName;

                UpdateDataColumns(table, propertyMaps);

                foreach (var data in dataSet)
                {
                    table.Rows.Add(BuildDataRow(table, data, propertyMaps));
                }

                table.EndLoadData();
                table.AcceptChanges();

                return table;
            }

            private static void UpdateDataColumns(DataTable table, IEnumerable<IPropertyMap> propertyMaps)
            {
                foreach (var propertyMap in propertyMaps.Where(p => !p.Ignored))
                {
                    var propertyType = propertyMap.PropertyInfo.PropertyType;
                    table.Columns.Add(propertyMap.ColumnName, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }
            }

            private static DataRow BuildDataRow<T>(DataTable table, T data, IEnumerable<IPropertyMap> propertyMaps) where T : class
            {
                var row = table.NewRow();
                row.BeginEdit();

                foreach (var propertyMap in propertyMaps.Where(p => !p.Ignored))
                {
                    row[propertyMap.ColumnName] = propertyMap.PropertyInfo.GetValue(data) ?? DBNull.Value;
                }

                row.EndEdit();
                row.AcceptChanges();

                return row;
            }
        }
    }
}