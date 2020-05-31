using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Cosmos.Data.Sx.Oracle
{
    /// <summary>
    /// Extensions for OracleClient
    /// </summary>
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Add with value
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AddWithValue(this OracleParameterCollection coll, string parameterName, object value)
        {
            coll.CheckNull(nameof(coll));
            coll.Add(parameterName, value);
        }

        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this OracleParameterCollection coll, Dictionary<string, object> values)
        {
            coll.CheckNull(nameof(coll));
           
            foreach (var keyValuePair in values)
            {
                coll.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}