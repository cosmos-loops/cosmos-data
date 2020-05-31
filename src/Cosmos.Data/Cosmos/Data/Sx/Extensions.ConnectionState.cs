using System.Data;
using Cosmos.Collections;

namespace Cosmos.Data.Sx
{
    /// <summary>
    /// Extensions for <see cref="ConnectionState"/>
    /// </summary>
    public static class ConnectionStateExtensions
    {
        /// <summary>
        /// In
        /// </summary>
        /// <param name="state"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In(this ConnectionState state, params ConnectionState[] values)
        {
            return values.IndexOf(state) != -1;
        }

        /// <summary>
        /// Not in
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn(this ConnectionState @this, params ConnectionState[] values)
        {
            return values.IndexOf(@this) == -1;
        }
    }
}