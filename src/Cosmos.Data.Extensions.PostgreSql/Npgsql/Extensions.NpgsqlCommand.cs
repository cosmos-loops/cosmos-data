using System.Data;
using System.Threading.Tasks;
using Cosmos;

namespace Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
    {
        #region Execute DataSet

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this NpgsqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new NpgsqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(this NpgsqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new NpgsqlDataAdapter(command);
            await Task.Run(() => dataAdapter.Fill(ds));

            return ds;
        }

        #endregion

        #region Execute DataTable

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this NpgsqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new NpgsqlDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(this NpgsqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new NpgsqlDataAdapter(command);
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
        internal static DataTable ExecuteFirstDataTable(this NpgsqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new NpgsqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds.Tables[0];
        }

        /// <summary>
        /// Execute DataTable async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static async Task<DataTable> ExecuteFirstDataTableAsync(this NpgsqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new NpgsqlDataAdapter(command);
            await Task.Run(() => dataAdapter.Fill(ds));

            return ds.Tables[0];
        }

        #endregion
    }
}