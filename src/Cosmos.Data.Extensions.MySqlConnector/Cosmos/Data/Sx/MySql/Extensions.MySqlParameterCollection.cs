using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Cosmos.Data.Sx.MySql
{
    /// <summary>
    /// Extensions for MySql
    /// </summary>
    public static partial class MySqlClientExtensions
    {
        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this MySqlParameterCollection conn, Dictionary<string, object> values)
        {
            conn.CheckNull(nameof(conn));

            foreach (var keyValuePair in values)
            {
                conn.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}