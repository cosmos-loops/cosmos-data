using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Cosmos.Data.Sx.SQLite
{
    /// <summary>
    /// Extensions for Sqlite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class SQLiteExtensions
    {
        #region Execute DataSet

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command">The @this to act on.</param>
        /// <returns>A DataSet that is equivalent to the result set.</returns>
        public static DataSet ExecuteDataSet(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command">The @this to act on.</param>
        /// <returns>A DataSet that is equivalent to the result set.</returns>
        public static async Task<DataSet> ExecuteDataSetAsync(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SQLiteDataAdapter(command);
            await Task.Run(() => dataAdapter.Fill(ds));

            return ds;
        }

        #endregion

        #region Execute DataTable

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command">The @this to act on.</param>
        /// <returns>A DataTable that is equivalent to the first result set.</returns>
        public static DataTable ExecuteDataTable(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command">The @this to act on.</param>
        /// <returns>A DataTable that is equivalent to the first result set.</returns>
        public static async Task<DataTable> ExecuteDataTableAsync(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new SQLiteDataAdapter(command);
            await Task.Run(() => dataAdapter.Fill(dt));

            return dt;
        }

        #endregion

        #region Internal Execute DataTable

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static DataTable ExecuteFirstDataTable(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds.Tables[0];
        }

        /// <summary>
        /// Execute DataTable async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static async Task<DataTable> ExecuteFirstDataTableAsync(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SQLiteDataAdapter(command);
            await Task.Run(() => dataAdapter.Fill(ds));

            return ds.Tables[0];
        }

        #endregion

        #region Execute DataReader

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Task<SQLiteDataReader> ExecuteReaderAsync(this SQLiteCommand command)
        {
            command.CheckNull(nameof(command));
            return Task.Run(command.ExecuteReader);
        }

        #endregion
    }
}