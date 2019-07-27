using System.Collections.Generic;

namespace Oracle.ManagedDataAccess.Client
{
    public static partial class OracleClientExtensions
    {
        public static void AddWithValue(this OracleParameterCollection @this, string parameterName, object value)
        {
            @this.Add(parameterName, value);
        }

        public static void AddRangeWithValue(this OracleParameterCollection @this, Dictionary<string, object> values)
        {
            foreach (var keyValuePair in values)
            {
                @this.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}