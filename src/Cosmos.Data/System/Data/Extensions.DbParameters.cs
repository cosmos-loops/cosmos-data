using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace System.Data
{
    /// <summary>
    /// Extensions for DbParameters
    /// </summary>
    public static class DbParametersExtensions
    {
        /// <summary>
        /// TO DbParameters
        /// </summary>
        /// <param name="this"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> @this, DbCommand command)
        {
            return @this.Select(x =>
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = x.Key;
                parameter.Value = x.Value;
                return parameter;
            }).ToArray();
        }

        /// <summary>
        /// To DbParameters
        /// </summary>
        /// <param name="this"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> @this, DbConnection connection)
        {
            var command = connection.CreateCommand();

            return @this.Select(x =>
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = x.Key;
                parameter.Value = x.Value;
                return parameter;
            }).ToArray();
        }
    }
}