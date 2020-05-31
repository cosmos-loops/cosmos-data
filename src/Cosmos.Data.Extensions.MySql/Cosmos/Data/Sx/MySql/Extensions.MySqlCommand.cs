using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Cosmos.Data.Sx.MySql
{
    /// <summary>
    /// Extensions for MySql
    /// </summary>
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
        /// Execute DataSet async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new MySqlDataAdapter(command);
            await dataAdapter.FillAsync(ds);

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
        /// Execute DataTable async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(this MySqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new MySqlDataAdapter(command);
            await dataAdapter.FillAsync(dt);

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
            await dataAdapter.FillAsync(ds);

            return ds.Tables[0];
        }

        #endregion
    }
}