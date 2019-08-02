using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/*
 * Reference to:
 *     herryh/SQLiteBulkCopy
 *     Author: Herry Hamidjaja
 *     Url: https://github.com/herryh/SQLiteBulkCopy
 *     MIT
 */

namespace System.Data.SQLite.SqlBulkCopy
{
    /// <summary>
    /// Lets you efficiently bulk load a Sqlite table with data from another source.
    /// Based on https://github.com/aspnet/Microsoft.Data.Sqlite/issues/289
    /// 
    /// Modification is made for it to support BLOB bulk insert. So that it will be more generic.
    /// 
    /// Modified by: Herry Hamidjaja
    /// Date: 12/08/2018 
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLiteBulkCopy : IDisposable
    {

        #region "Field(s)"

        private bool _defaultCloseAndDisploseDataReader = true;
        private int _defaultBatchSize = 5000;
        private int _defaultCopyTimeout = 60; // seconds
        private SQLiteConnection _cn;
        private string _tableNm;
        private Dictionary<string, SQLiteColumnType> _columns;

        /// <summary>
        /// Gets column mappings
        /// </summary>
        public Dictionary<string, SQLiteColumnType> ColumnMappings => _columns;

        #endregion

        #region "Properties"

        /// <summary>
        /// Name of the destination table in the database.
        /// </summary>
        public string DestinationTableName
        {
            get { return _tableNm; }
            set
            {
                _tableNm = value;
                if (!string.IsNullOrEmpty(value))
                    SetColumnMetaData();
            }
        }

        /// <summary>
        /// Allows control of the incoming DataReader by closing and disposing of it by default after all bulk copy 
        /// operations have completed if set to TRUE, if set to FALSE you need to do your own cleanup (this is useful 
        /// when your DataReader returns more than one result set).  By default this is set to TRUE.
        /// </summary>
        public bool CloseAndDisploseDataReader { get; set; }

        /// <summary>
        /// Batch size data is chunked into when inserting.  The default size is 1,000 records per batch.
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// The time in seconds to wait for a batch to load. The default is 30 seconds.
        /// </summary>
        public int BulkCopyTimeout { get; set; }

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the SqliteBulkCopy class using the specified open instance of SqliteConnection.
        /// </summary>
        /// <param name="connection"></param>
        public SQLiteBulkCopy(SQLiteConnection connection)
        {
            CloseAndDisploseDataReader = _defaultCloseAndDisploseDataReader;
            BatchSize = _defaultBatchSize;
            BulkCopyTimeout = _defaultCopyTimeout;
            _cn = connection;
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
        }

        /// <summary>
        /// Initializes and opens a new instance of SqliteBulkCopy based on the supplied connectionString. 
        /// The constructor uses the SqliteConnection to initialize a new instance of the SqliteBulkCopy class.
        /// </summary>
        /// <param name="connectionString"></param>
        public SQLiteBulkCopy(string connectionString)
        {
            CloseAndDisploseDataReader = _defaultCloseAndDisploseDataReader;
            BatchSize = _defaultBatchSize;
            BulkCopyTimeout = _defaultCopyTimeout;
            _cn = new SQLiteConnection(connectionString);
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
        }

        #endregion

        #region "Method(s) and Enum(s)"

        /// <summary>
        /// Get column metadata for a table in a Sqlite database
        /// </summary>
        private void SetColumnMetaData()
        {
            if (string.IsNullOrWhiteSpace(DestinationTableName))
                return;

            _columns = new Dictionary<string, SQLiteColumnType>();

            var sql = $"pragma table_info('{DestinationTableName}')";

            using (var cmd = new SQLiteCommand(sql, _cn) {CommandType = CommandType.Text})
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var key = dr["name"].ToString();
                        // data types found @ http://www.tutorialspoint.com/sqlite/sqlite_data_types.htm
                        var typ = SQLiteColumnTypeConverter.ToSqliteColumnType(dr["type"].ToString().ToUpper());

                        _columns.Add(key, typ);
                    }
                }
            }

            if (_columns == null || _columns.Count < 1)
                throw new Exception($"{DestinationTableName} could not be found in the database");
        }

        /// <summary>
        /// Close and database connections.
        /// </summary>
        public void Close()
        {
            if (_cn.State == ConnectionState.Open)
                _cn.Close();
        }

        /// <summary>
        /// Copies all rows in the supplied IDataReader to a destination table specified 
        /// by the DestinationTableName property of the SqliteBulkCopy object.
        /// </summary>
        /// <param name="reader"></param>
        public void WriteToServer(IDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            // build the insert schema
            var insertClause = new StringBuilder();
            insertClause.Append($"INSERT INTO {DestinationTableName} (");
            var first = true;
            foreach (var c in ColumnMappings)
            {
                if (first)
                {
                    insertClause.Append(c.Key);
                    first = false;
                }
                else
                {
                    insertClause.Append("," + c.Key);
                }
            }

            insertClause.Append(")");

            // ReSharper disable once RedundantAssignment
            first = true;
            var valuesClause = new StringBuilder();
            var currentBatch = 0;
            while (reader.Read())
            {
                // generate insert values block statement
                if (currentBatch > 0)
                    valuesClause.Append(",");
                valuesClause.Append("(");
                var colFirst = true;
                foreach (var c in ColumnMappings)
                {
                    if (!colFirst)
                        valuesClause.Append(",");
                    else
                        colFirst = false;
                    var columnValue = reader[c.Key] == null ? null : reader[c.Key].ToSQLiteString();
                    if (string.IsNullOrEmpty(columnValue))
                        valuesClause.Append("NULL");
                    else
                    {
                        switch (c.Value)
                        {
                            case SQLiteColumnType.Date:
                                try
                                {
                                    valuesClause.Append($"'{DateTime.Parse(columnValue):yyyy-MM-dd HH:mm:ss}'");
                                }
                                catch (Exception exp)
                                {
                                    throw new Exception(
                                        $"Invalid Cast when loading date column [{c.Key}] in table [{DestinationTableName}] in Sqlite DB with data; value being casted '{columnValue}', incoming values must be of data format consumable by .NET; error:\n {exp.Message}");
                                }

                                break;
                            case SQLiteColumnType.Integer:
                            case SQLiteColumnType.Numeric:
                            case SQLiteColumnType.Real:
                                valuesClause.Append(columnValue);
                                break;
                            case SQLiteColumnType.Boolean:
                                // ReSharper disable once RedundantAssignment
                                var out_value = -1;
                                if (columnValue.ToUpper() == "TRUE")
                                    valuesClause.Append("1");
                                else if (columnValue.ToUpper() == "FALSE")
                                    valuesClause.Append("0");
                                else if (int.TryParse(columnValue, out out_value))
                                {
                                    if (out_value == 1 || out_value == 0)
                                        valuesClause.Append($"{columnValue}");
                                    else // numeric value out of range, throw exception
                                        throw new Exception(
                                            $"Invalid Cast when loading boolean column [{c.Key}] in table [{DestinationTableName}] in Sqlite DB with data; value being casted '{columnValue}', incoming values can only be True or False (case does not matter), 1 (true) or 0 (false), or NULL");
                                }
                                else // no valid boolean types throw exception
                                    throw new Exception(
                                        $"Invalid Cast when loading boolean column [{c.Key}] in table [{DestinationTableName}] in Sqlite DB with data; value being casted '{columnValue}', incoming values can only be True or False (case does not matter), 1 (true) or 0 (false), or NULL");

                                break;
                            case SQLiteColumnType.Blob:
                                valuesClause.Append($"X'{columnValue}'");
                                break;
                            // ReSharper disable once RedundantCaseLabel
                            case SQLiteColumnType.Text:
                            default:
                                valuesClause.Append($"'{columnValue.Replace("'", "''")}'");
                                break;
                        }
                    }
                }

                valuesClause.Append(")");

                currentBatch++;
                if (currentBatch == this.BatchSize)
                {
                    var dml = $"BEGIN;\n{insertClause} VALUES {valuesClause};\nCOMMIT;";
                    valuesClause.Clear();
                    using (var cmd = new SQLiteCommand(dml, _cn) {CommandType = CommandType.Text, CommandTimeout = BulkCopyTimeout})
                        cmd.ExecuteNonQuery();
                    currentBatch = 0;
                }
            }

            if (CloseAndDisploseDataReader)
            {
                reader.Close();
                reader.Dispose();
            }

            // if any records remain after the read loop has completed then write them to the DB
            if (currentBatch > 0)
            {
                var dml = $"BEGIN;\n{insertClause} VALUES {valuesClause};\nCOMMIT;";
                using (var cmd = new SQLiteCommand(dml, _cn) {CommandType = CommandType.Text, CommandTimeout = BulkCopyTimeout})
                    cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// Write to server async
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public async Task WriteToServerAsync(IDataReader reader)
        {
            await WriteToServerAsyncInternal(reader);
        }

#pragma warning disable 1998
        /// <summary>
        /// The asynchronous version of WriteToServer, which copies all rows in the supplied IDataReader to a 
        /// destination table specified by the DestinationTableName property of the SqliteBulkCopy object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private async Task WriteToServerAsyncInternal(IDataReader reader)
        {
            WriteToServer(reader);
        }
#pragma warning restore 1998

        /// <summary>
        /// Releases all resources used by the current instance of the SqliteBulkCopy class.
        /// </summary>
        public void Dispose()
        {
            Close();
            _cn.Dispose();
        }

        #endregion

    }
}