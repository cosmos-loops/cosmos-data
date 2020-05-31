using System.Collections.Generic;
using System.Data.SQLite;

namespace Cosmos.Data.Sx.SQLite
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

            foreach (var keyValuePair in values)
            {
                conn.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}