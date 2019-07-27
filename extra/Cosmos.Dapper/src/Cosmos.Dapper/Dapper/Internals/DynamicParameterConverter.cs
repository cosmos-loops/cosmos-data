using System.Collections.Generic;

namespace Dapper.Internals
{
    internal static class DynamicParameterConverter
    {
        public static DynamicParameters ToDynamicParameters(this IDictionary<string, object> parameters)
        {
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
                dynamicParameters.Add(parameter.Key, parameter.Value);
            return dynamicParameters;
        }
    }
}