using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
    /// <summary>
    /// Extensions for OracleClient
    /// </summary>
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this OracleCommand @this)
        {
            var ds = new DataSet();
            using (var dataAdapter = new OracleDataAdapter(@this))
            {
                dataAdapter.Fill(ds);
            }

            return ds;
        }

        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(this OracleCommand @this)
        {
            var dt = new DataTable();
            using (var dataAdapter = new OracleDataAdapter(@this))
            {
                dataAdapter.Fill(dt);
            }

            return dt;
        }
    }
}