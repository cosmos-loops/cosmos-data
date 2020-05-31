using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cosmos.Data.Sx.SqlClient
{
    public static partial class SqlClientExtensions
    {
        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this SqlParameterCollection conn, Dictionary<string, object> values)
        {
            conn.CheckNull(nameof(conn));
            
            foreach (var keyValuePair in values)
            {
                conn.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}