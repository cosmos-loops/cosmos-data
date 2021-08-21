using System.Data;
using System.Threading.Tasks;
using Cosmos;

namespace MySqlConnector
{
    public static partial class MySqlClientExtensions
    {
        #region Execute DataSet

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new MySqlDataAdapter(command);
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
        public static DataTable ExecuteDataTable(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new MySqlDataAdapter(command);
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
        internal static DataTable ExecuteFirstDataTable(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds.Tables[0];
        }

        /// <summary>
        /// Execute DataTable async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static async Task<DataTable> ExecuteFirstDataTableAsync(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new MySqlDataAdapter(command);
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
        public static Task<MySqlDataReader> ExecuteReaderAsync(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            return Task.Run(command.ExecuteReader);
        }

        #endregion
    }
}