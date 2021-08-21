using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

/*
 * Reference to:
 *     TylerHaigh/OracleBulkCopy
 *     URL: https://github.com/TylerHaigh/OracleBulkCopy
 *     Author: Tyler Haigh
 *     MIT
 */

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    /// <summary>
    /// Oracle BulkCopy
    /// </summary>
    public class OracleBulkCopy : IDisposable
    {
        // https://github.com/Microsoft/referencesource/blob/master/System.Data/System/Data/SqlClient/SqlBulkCopy.cs
        // https://stackoverflow.com/questions/47942691/how-to-make-a-bulk-insert-using-oracle-managed-data-acess-c-sharp
        // https://github.com/DigitalPlatform/dp2/blob/master/DigitalPlatform.rms.db/OracleBulkCopy.cs
        // https://msdn.microsoft.com/en-us/library/system.data.oracleclient.oracletype(v=vs.110).aspx

        private OracleConnection _connection;

        // ReSharper disable once InconsistentNaming
        private OracleTransaction _externalTransaction { get; set; }

        /// <summary>
        /// Set to TRUE if the BulkCopy object was not instantiated with an external OracleConnection
        /// and thus it is up to the BulkCopy object to open and close connections
        /// </summary>
        private bool _ownsTheConnection;

        /// <summary>
        /// Create a new instance of <see cref="OracleBulkCopy"/>
        /// </summary>
        /// <param name="connectionString"></param>
        public OracleBulkCopy(string connectionString) : this(new OracleConnection(connectionString))
        {
            _ownsTheConnection = true;
        }

        /// <summary>
        /// Create a new instance of <see cref="OracleBulkCopy"/>
        /// </summary>
        /// <param name="connection"></param>
        public OracleBulkCopy(OracleConnection connection) : this(connection, null) { }

        /// <summary>
        /// Create a new instance of <see cref="OracleBulkCopy"/>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        public OracleBulkCopy(OracleConnection connection, OracleTransaction transaction = null)
        {
            _connection = connection;
            _externalTransaction = transaction;
        }

        private string _destinationTableName;

        /// <summary>
        /// Destination TableName
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public string DestinationTableName
        {
            get => _destinationTableName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Destination Table Name cannot be null or empty string");
                _destinationTableName = value;
            }
        }

        private int _batchSize;

        /// <summary>
        /// Batck size
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public int BatchSize
        {
            get => _batchSize;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Batch Size must be a positive integer");
                _batchSize = value;
            }
        }

        private int _timeout = 30;

        /// <summary>
        /// BulkCopy timeout
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public int? BulkCopyTimeout
        {
            get => _timeout;
            set
            {
                if (value is < 0)
                    throw new ArgumentException("Timeout value must be a postive integer");
                _timeout = value ?? 30;
            }
        }

        private bool UploadEverythingInSingleBatch => _batchSize == 0;

        private void ValidateConnection()
        {
            if (_connection is null)
                throw new Exception("Oracle Database Connection is required");

            if (_externalTransaction is not null && _externalTransaction.Connection != _connection)
                throw new Exception("Oracle Transaction mismatch with Oracle Database Connection");
        }

        private void OpenConnection()
        {
            if (_ownsTheConnection && _connection.State != ConnectionState.Open)
                _connection.Open();
        }

        // TODO: Implement WriteToServer for a IDataReader input

        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="table"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void WriteToServer(DataTable table)
        {
            // https://stackoverflow.com/questions/47942691/how-to-make-a-bulk-insert-using-oracle-managed-data-acess-c-sharp
            // https://github.com/Microsoft/referencesource/blob/master/System.Data/System/Data/SqlClient/SqlBulkCopy.cs

            if (table is null)
                throw new ArgumentNullException(nameof(table));

            // TODO: Validate TableName to prevent SQL Injection
            // https://oracle-base.com/articles/10g/dbms_assert_10gR2
            // SELECT SYS.DBMS_ASSERT.qualified_sql_name('object_name') FROM dual;

            if (UploadEverythingInSingleBatch)
                WriteToServerInSingleBatch(table);
            else
                WriteToServerInMultipleBatches(table);
        }

        private void WriteToServerInSingleBatch(DataTable table)
        {
            // Build the command string
            var commandText = BuildCommandText(table);

            WriteSingleBatchOfData(table, 0, commandText, table.Rows.Count, _timeout);
        }

        private void WriteToServerInMultipleBatches(DataTable table)
        {
            // Calculate number of batches
            var numBatchesRequired = (int)Math.Ceiling(table.Rows.Count / (double)BatchSize);

            // Build the command string
            var commandText = BuildCommandText(table);

            for (var i = 0; i < numBatchesRequired; i++)
            {
                var skipOffset = i * BatchSize;
                var batchSize = Math.Min(BatchSize, table.Rows.Count - skipOffset);
                WriteSingleBatchOfData(table, skipOffset, commandText, batchSize, _timeout);
            }
        }

        private string BuildCommandText(DataTable table)
        {
            // Build the command string
            var commandText = $"Insert Into {DestinationTableName} ( @@ColumnList@@ ) Values ( @@ValueList@@ )";
            var columnList = GetColumnList(table);
            var valueList = GetValueList(table);

            // Replace the placeholders with actual values
            commandText = commandText.Replace("@@ColumnList@@", columnList);
            commandText = commandText.Replace("@@ValueList@@", valueList);

            // TODO: Validate commandText to prevent SQL Injection
            // https://oracle-base.com/articles/10g/dbms_assert_10gR2

            return commandText;
        }

        private void WriteSingleBatchOfData(DataTable table, int skipOffset, string commandText, int batchSize, int timeout)
        {
            // Get array of row data for all columns in the table
            var parameters = GetParameters(table, batchSize, skipOffset);

            // Create the OracleCommand and bind the data
            var cmd = _connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.ArrayBindCount = batchSize;
            cmd.CommandTimeout = timeout;
            parameters.ForEach(p => cmd.Parameters.Add(p));

            // Validate and open the connection
            ValidateConnection();
            OpenConnection();

            // Upload the data
            cmd.ExecuteNonQuery();

            // Commit Transaction
            //CommitTransaction(); // ????
        }

        private List<OracleParameter> GetParameters(DataTable data, int batchSize, int skipOffset = 0)
        {
            var parameters = new List<OracleParameter>();
            foreach (DataColumn c in data.Columns)
            {
                var dbType = GetOracleDbTypeFromDotnetType(c.DataType);

                // https://stackoverflow.com/a/23735845/2442468
                // https://stackoverflow.com/a/17595403/2442468

                var columnData = data.AsEnumerable().Select(r => r.Field<object>(c.ColumnName));
                var paramDataArray = UploadEverythingInSingleBatch
                    ? columnData.ToArray()
                    : columnData.Skip(skipOffset).Take(batchSize).ToArray();

                var param = new OracleParameter { OracleDbType = dbType, Value = paramDataArray };

                parameters.Add(param);
            }

            return parameters;
        }

        private string GetColumnList(DataTable data)
        {
            var columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            var columnList = Joiners.Joiner.On(',').Join(columnNames);
            return columnList;
        }

        private string GetValueList(DataTable data)
        {
            const string delimiter = ", ";

            var sb = new StringBuilder();
            for (var i = 1; i <= data.Columns.Count; i++)
            {
                sb.Append($":{i}");
                sb.Append(delimiter);
            }

            sb.Length -= delimiter.Length;

            var valueList = sb.ToString();
            return valueList;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_connection is not null)
            {
                // Only close the connection if the BulkCopy instance owns the connection
                if (_ownsTheConnection)
                    _connection.Dispose();

                // Always set to null
                _connection = null;
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        // ReSharper disable once UnusedMember.Local
        private static OracleDbType GetOracleDbType(object o)
        {
            // https://stackoverflow.com/questions/1583150/c-oracle-data-type-equivalence-with-oracledbtype#1583197
            // https://docs.oracle.com/cd/B19306_01/win.102/b14307/OracleDbTypeEnumerationType.htm
            // https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/oracle-data-type-mappings

            if (o is byte[]) return OracleDbType.Blob;
            if (o is string) return OracleDbType.Varchar2;
            if (o is DateTime) return OracleDbType.Date;
            if (o is decimal) return OracleDbType.Decimal;
            if (o is int) return OracleDbType.Int32;

            if (o is long) return OracleDbType.Int64;
            if (o is short) return OracleDbType.Int16;
            if (o is sbyte) return OracleDbType.Byte;
            if (o is byte) return OracleDbType.Int16; // <== unverified
            if (o is float) return OracleDbType.Single;
            if (o is double) return OracleDbType.Double;

            // Tylers
            //if (o is bool) return OracleDbType.Boolean;
            //if (o is char) return OracleDbType.Char;

            return OracleDbType.Varchar2;
        }

        private static OracleDbType GetOracleDbTypeFromDotnetType(Type t)
        {
            if (t == typeof(byte[])) return OracleDbType.Blob;
            if (t == typeof(string)) return OracleDbType.Varchar2;
            if (t == typeof(DateTime)) return OracleDbType.Date;
            if (t == typeof(decimal)) return OracleDbType.Decimal;
            if (t == typeof(int)) return OracleDbType.Int32;

            if (t == typeof(long)) return OracleDbType.Int64;
            if (t == typeof(short)) return OracleDbType.Int16;
            if (t == typeof(sbyte)) return OracleDbType.Byte;
            if (t == typeof(byte)) return OracleDbType.Int16; // <== unverified
            if (t == typeof(float)) return OracleDbType.Single;
            if (t == typeof(double)) return OracleDbType.Double;

            // Tylers
            //if (o is bool) return OracleDbType.Boolean;
            //if (o is char) return OracleDbType.Char;

            return OracleDbType.Varchar2;
        }
    }
}