using System.Collections.Generic;
using Cosmos;

namespace MySql.Data.MySqlClient
{
    public static partial class MySqlClientExtensions
    {
        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this MySqlParameterCollection coll, Dictionary<string, object> values)
        {
            coll.CheckNull(nameof(coll));
#if NETFRAMEWORK || NETSTANDARD2_0
            foreach (var pair in values)
            {
                var key = pair.Key;
                var value = pair.Value;
#else
            foreach (var (key, value) in values)
            {
#endif
                coll.AddWithValue(key, value);
            }
        }
    }
}