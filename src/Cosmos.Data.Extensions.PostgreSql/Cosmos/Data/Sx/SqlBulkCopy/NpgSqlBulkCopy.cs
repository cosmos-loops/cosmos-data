using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Text;
using Npgsql;

namespace Cosmos.Data.Sx.SqlBulkCopy
{
    public class NpgSqlBulkCopy : IDisposable
    {
        private NpgsqlConnection _conn;
        private NpgsqlTransaction _externalTransaction { get; set; }

        /// <summary>
        /// Set to TRUE if the BulkCopy object was not instantiated with an external OracleConnection
        /// and thus it is up to the BulkCopy object to open and close connections
        /// </summary>
        private bool _ownsTheConnection;

        /// <summary>
        /// Create a new instance of <see cref="NpgSqlBulkCopy"/>
        /// </summary>
        /// <param name="connectionString"></param>
        public NpgSqlBulkCopy(string connectionString) : this(new NpgsqlConnection(connectionString))
        {
            _ownsTheConnection = true;
        }

        /// <summary>
        /// Create a new instance of <see cref="NpgSqlBulkCopy"/>
        /// </summary>
        /// <param name="connection"></param>
        public NpgSqlBulkCopy(NpgsqlConnection connection) : this(connection, null) { }

        /// <summary>
        /// Create a new instance of <see cref="NpgSqlBulkCopy"/>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        public NpgSqlBulkCopy(NpgsqlConnection connection, NpgsqlTransaction transaction = null)
        {
            _conn = connection;
            _externalTransaction = transaction;
        }

        #region TableName

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

        private string GetTableName(DataTable table)
        {
            if (DestinationTableName.IsNullOrWhiteSpace())
                return table.TableName;
            return DestinationTableName;
        }

        #endregion

        #region Internal
        
        private NpgsqlBinaryImporter BuildImporter(DataTable table)
        {
            var colNames = table.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray();
            var colNameSegment = Joiners.Joiner.On(',').Join(colNames);
            var writer = _conn.BeginBinaryImport($"COPY {GetTableName(table)} ({colNameSegment}) FROM STDIN (FORMAT BINARY)");

            foreach (DataRow dataRow in table.Rows)
            {
                writer.StartRow();
                foreach (var colName in colNames)
                {
                    writer.Write(dataRow[colName]);
                }
            }

            return writer;
        }

        #endregion

        #region WriteToServer

        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="table"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void WriteToServer(DataTable table)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            var needAutoCloseConn = _conn.OpenIfNeeded();
            var tx = _externalTransaction ?? _conn.BeginTransaction();

            using (tx)
            {
                try
                {
                    using (var writer = BuildImporter(table))
                    {
                        writer.Complete();
                        writer.Close();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="reader"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void WriteToServer(IDataReader reader)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));

            WriteToServer(reader.ToDataTable());
        }

        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task WriteToServerAsync(IDataReader reader, CancellationToken cancellationToken = default)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));

            await WriteToServerAsync(reader.ToDataTable(), cancellationToken);
        }

#if NET452
        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="table"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken = default)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            var needAutoCloseConn = await _conn.OpenIfNeededAsync(cancellationToken);
            var tx = _externalTransaction ?? _conn.BeginTransaction();

            try
            {
                using (var writer = BuildImporter(table))
                {
                    writer.Complete();
                    writer.Close();
                }

                await tx.CommitAsync(cancellationToken);
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }

            await _conn.CloseIfNeededAsync(needAutoCloseConn);
        }
#elif NETFRAMEWORK
        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="table"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken = default)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            var needAutoCloseConn = await _conn.OpenIfNeededAsync(cancellationToken);
            var tx = _externalTransaction ?? _conn.BeginTransaction();

            try
            {
                await using (var writer = BuildImporter(table))
                {
                    await writer.CompleteAsync(cancellationToken);
                    await writer.CloseAsync(cancellationToken);
                }

                await tx.CommitAsync(cancellationToken);
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }

            await _conn.CloseIfNeededAsync(needAutoCloseConn);
        }
#else
        /// <summary>
        /// Write to server
        /// </summary>
        /// <param name="table"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken = default)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            var needAutoCloseConn = await _conn.OpenIfNeededAsync(cancellationToken);
            var tx = _externalTransaction ?? await _conn.BeginTransactionAsync(cancellationToken);

            try
            {
                await using (var writer = BuildImporter(table))
                {
                    await writer.CompleteAsync(cancellationToken);
                    await writer.CloseAsync(cancellationToken);
                }

                await tx.CommitAsync(cancellationToken);
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }

            await _conn.CloseIfNeededAsync(needAutoCloseConn);
        }
#endif
        
        #endregion

        #region Dispose

        /// <summary>
        /// Close and database connections.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Releases all resources used by the current instance of the SqliteBulkCopy class.
        /// </summary>
        public void Dispose()
        {
            if (_conn is not null)
            {
                // Only close the connection if the BulkCopy instance owns the connection
                if (_ownsTheConnection)
                    _conn.Dispose();

                // Always set to null
                _conn = null;
            }
        }

        #endregion
    }
}