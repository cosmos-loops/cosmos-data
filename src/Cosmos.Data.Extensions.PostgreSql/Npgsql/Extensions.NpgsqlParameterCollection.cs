using System.Collections.Generic;
using Cosmos;

namespace Npgsql
{
    /// <summary>
    /// Extensions for Npgsql
    /// </summary>
    public static partial class NpgsqlClientExtensions
    {
        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this NpgsqlParameterCollection conn, Dictionary<string, object> values)
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