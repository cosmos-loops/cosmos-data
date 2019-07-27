using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MySql.Data.MySqlClient.SqlBulkCopy
{
    public class MySqlBulkCopy : IDisposable
    {
        private readonly MySqlConnection _connection;

        public MySqlBulkCopy(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            FilePathHistory = new List<string>();
        }

        public string DestinationTableName { get; set; }

        public string SecureFilePriv { get; set; } = null;

        public int BulkCopyTimeout { get; set; }

        public bool ClearTempCsvAfterWriting { get; set; } = true;

        private string CurrentFilePath { get; set; }
        private List<string> FilePathHistory { get; set; }

        public void WriteToServer(DataTable table)
        {
            var bulkLoader = MySqlBulkCopyHelper.GetBulkLoader(_connection, table, DestinationTableName, BulkCopyTimeout, SecureFilePriv);

            UpdateCurrentFilePath(bulkLoader.FileName);
            UpdateColumnInfo(bulkLoader, table);

            bulkLoader.Load();
        }

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