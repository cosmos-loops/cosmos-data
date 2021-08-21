using System.Collections.Generic;
using Cosmos;

namespace MySqlConnector
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

#if NETFRAMEWORK || NETSTANDARD2_0
            foreach (var pair in values)
            {
                var key = pair.Key;
                var value = pair.Value;
#else
            foreach (var (key, value) in values)
            {
#endif
                conn.AddWithValue(key, value);
            }
        }
    }
}