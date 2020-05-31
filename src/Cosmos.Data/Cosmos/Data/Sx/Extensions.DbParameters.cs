using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for DbParameters
    /// </summary>
    public static class DbParametersExtensions
    {
        /// <summary>
        /// TO DbParameters
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> dict, DbCommand command)
        {
            dict.CheckNull(nameof(dict));
            command.CheckNull(nameof(command));

            return dict.Select(x =>
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
        /// <param name="dict"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static DbParameter[] ToDbParameters(this IDictionary<string, object> dict, DbConnection connection)
        {
            dict.CheckNull(nameof(dict));
            connection.CheckNull(nameof(connection));

            var command = connection.CreateCommand();

            return dict.Select(x =>
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = x.Key;
                parameter.Value = x.Value;
                return parameter;
            }).ToArray();
        }
    }
}