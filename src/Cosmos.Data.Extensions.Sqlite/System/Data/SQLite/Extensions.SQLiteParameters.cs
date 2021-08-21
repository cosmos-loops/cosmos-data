using System.Collections.Generic;
using Cosmos;

namespace System.Data.SQLite
{
    /// <summary>
    /// Extensions for Sqlite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class SQLiteExtensions
    {
        /// <summary>
        /// AddRangeW with value
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this SQLiteParameterCollection conn, Dictionary<string, object> values)
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