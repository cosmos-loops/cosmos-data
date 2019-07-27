using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        public static DataSet ExecuteDataSet(this OracleCommand @this)
        {
            var ds = new DataSet();
            using (var dataAdapter = new OracleDataAdapter(@this))
            {
                dataAdapter.Fill(ds);
            }

            return ds;
        }

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