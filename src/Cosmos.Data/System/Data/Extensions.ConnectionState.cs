namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="ConnectionState"/>
    /// </summary>
    public static class ConnectionStateExtensions
    {
        /// <summary>
        /// In
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In(this ConnectionState @this, params ConnectionState[] values)
            => values.IndexOf(@this) != -1;

        /// <summary>
        /// Not in
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn(this ConnectionState @this, params ConnectionState[] values)
            => values.IndexOf(@this) == -1;
    }
}