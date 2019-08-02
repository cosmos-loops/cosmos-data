using System.Collections.Generic;

namespace Oracle.ManagedDataAccess.Client
{
    /// <summary>
    /// Extensions for OracleClient
    /// </summary>
    public static partial class OracleClientExtensions
    {
        /// <summary>
        /// Add with value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AddWithValue(this OracleParameterCollection @this, string parameterName, object value)
        {
            @this.Add(parameterName, value);
        }

        /// <summary>
        /// Add range with value
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRangeWithValue(this OracleParameterCollection @this, Dictionary<string, object> values)
        {
            foreach (var keyValuePair in values)
            {
                @this.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}