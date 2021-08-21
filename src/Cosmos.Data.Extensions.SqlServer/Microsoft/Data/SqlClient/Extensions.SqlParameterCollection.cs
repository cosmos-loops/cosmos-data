using System.Collections.Generic;
using Cosmos;

#if NET451 || NET452
// ReSharper disable once CheckNamespace
namespace System.Data.SqlClient
{
#else
namespace Microsoft.Data.SqlClient
{
#endif
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