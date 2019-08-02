namespace System.Data
{
    /// <summary>
    /// Extensions for <see cref="IDbConnection"/>
    /// </summary>
    public static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Ensure open
        /// </summary>
        /// <param name="this"></param>
        public static void EnsureOpen(this IDbConnection @this)
        {
            if (@this.State == ConnectionState.Closed)
            {
                @this.Open();
            }
        }

        /// <summary>
        /// Is connection open
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsConnectionOpen(this IDbConnection @this)
            => @this.State == ConnectionState.Open;

        /// <summary>
        /// Is connection not open
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotConnectionOpen(this IDbConnection @this)
            => @this.State != ConnectionState.Open;
    }
}