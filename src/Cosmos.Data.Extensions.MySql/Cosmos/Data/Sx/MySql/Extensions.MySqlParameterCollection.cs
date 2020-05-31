using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Cosmos.Data.Sx.MySql
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

            foreach (var keyValuePair in values)
            {
                coll.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}