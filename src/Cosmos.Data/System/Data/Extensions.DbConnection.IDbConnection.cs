namespace System.Data
{
    public static partial class DbConnectionExtensions
    {
        public static void EnsureOpen(this IDbConnection @this)
        {
            if (@this.State == ConnectionState.Closed)
            {
                @this.Open();
            }
        }

        public static bool IsConnectionOpen(this IDbConnection @this)
        {
            return @this.State == ConnectionState.Open;
        }

        public static bool IsNotConnectionOpen(this IDbConnection @this)
        {
            return @this.State != ConnectionState.Open;
        }
    }
}