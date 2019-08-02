using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MySql.Data.MySqlClient.SqlBulkCopy
{
    /// <summary>
    /// MySql BulkCopy
    /// </summary>
    public class MySqlBulkCopy : IDisposable
    {
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Create a new instance of <see cref="MySqlBulkCopy"/>
        /// </summary>
        /// <param name="connection"></param>
        public MySqlBulkCopy(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            FilePathHistory = new List<string>();
        }

        /// <summary>
        /// Destination TableName
        /// </summary>
        public string DestinationTableName { get; set; }

        /// <summary>
        /// Secure FilePriv
        /// </summary>
        public string SecureFilePriv { get; set; } = null;

        /// <summary>
        /// BulkCopy timeout
        /// </summary>
        public int BulkCopyTimeout { get; set; }

        /// <summary>
        /// Clear temp csv after writing
        /// </summary>
        public bool ClearTempCsvAfterWriting { get; set; } = true;

        private string CurrentFilePath { get; set; }
        private List<string> FilePathHistory { get; set; }

        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="table"></param>
        public void WriteToServer(DataTable table)
        {
            var bulkLoader = MySqlBulkCopyHelper.GetBulkLoader(_connection, table, DestinationTableName, BulkCopyTimeout, SecureFilePriv);

            UpdateCurrentFilePath(bulkLoader.FileName);
            UpdateColumnInfo(bulkLoader, table);

            bulkLoader.Load();
        }

        /// <summary>
        /// Write to server async
        /// </summary>
        /// <param name="table"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken = default)
        {
            var bulkLoader = MySqlBulkCopyHelper.GetBulkLoader(_connection, table, DestinationTableName, BulkCopyTimeout, SecureFilePriv);

            UpdateCurrentFilePath(bulkLoader.FileName);
            UpdateColumnInfo(bulkLoader, table);

            await bulkLoader.LoadAsync(cancellationToken);
        }

        private void UpdateCurrentFilePath(string currentFilePath)
        {
            FilePathHistory.Add(CurrentFilePath);
            CurrentFilePath = currentFilePath;
        }

        private void UpdateColumnInfo(MySqlBulkLoader bulkLoader, DataTable table)
        {
            foreach (DataColumn dbCol in table.Columns)
            {
                bulkLoader.Columns.Add(dbCol.ColumnName);
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (ClearTempCsvAfterWriting)
            {
                UpdateCurrentFilePath(string.Empty);

                foreach (var path in FilePathHistory)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                FilePathHistory.Clear();
            }
        }
    }
}