using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cosmos.Data.Sx.SqlClient
{
    public static partial class SqlClientExtensions
    {
        #region Execute DataSet

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SqlDataAdapter(command);
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
        public static DataTable ExecuteDataTable(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            var dt = new DataTable();
            using var dataAdapter = new SqlDataAdapter(command);
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
        internal static DataTable ExecuteFirstDataTable(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds.Tables[0];
        }

        /// <summary>
        /// Execute DataTable async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static async Task<DataTable> ExecuteFirstDataTableAsync(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            var ds = new DataSet();
            using var dataAdapter = new SqlDataAdapter(command);
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
        public static Task<SqlDataReader> ExecuteReaderAsync(this SqlCommand command)
        {
            command.CheckNull(nameof(command));
            return Task.Run(command.ExecuteReader);
        }

        #endregion
    }
}