namespace System.Data
{
    /// <summary>
    /// Extensions for SqlDateTime
    /// </summary>
    public static class SqlDateExtensions
    {
        /// <summary>
        /// To SqlDate
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSqlDate(this DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return "NULL";
            return "'" + dt.ToString("yyyyMMdd") + "'";
        }

        /// <summary>
        /// To SqlDateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSqlDateTime(this DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return "NULL";
            if (DoubleHelper.IsZero(dt.TimeOfDay.TotalMilliseconds))
                return "'" + dt.ToString("yyyyMMdd") + "'";
            return "'" + dt.ToString("yyyyMMdd HH:mm:ss.fff") + "'";
        }

        /// <summary>
        /// To SqlDate
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSqlDate(this DateTime? dt)
        {
            if (!dt.HasValue || dt == DateTime.MinValue)
                return "NULL";
            return "'" + dt.Value.ToString("yyyyMMdd") + "'";
        }

        /// <summary>
        /// To SqlDateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSqlDateTime(this DateTime? dt)
        {
            if (!dt.HasValue || dt == DateTime.MinValue)
                return "NULL";
            if (DoubleHelper.IsZero(dt.Value.TimeOfDay.TotalMilliseconds))
                return "'" + dt.Value.ToString("yyyyMMdd") + "'";
            return "'" + dt.Value.ToString("yyyyMMdd HH:mm:ss.fff") + "'";
        }

        /// <summary>
        /// Is valid sql small datetime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsValidSqlSmallDateTime(this DateTime dt)
        {
            var minSmallDateTime = new DateTime(1900, 1, 1);
            var maxSmallDateTime = new DateTime(2079, 6, 6);
            return dt >= minSmallDateTime && dt <= maxSmallDateTime;
        }

        /// <summary>
        /// Is valid sql datetime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsValidSqlDateTime(this DateTime dt)
        {
            var minDateTime = new DateTime(1753, 1, 1);
            var maxDateTime = new DateTime(9999, 12, 31);
            return dt >= minDateTime && dt <= maxDateTime;
        }

        /// <summary>
        /// This helper is a copy from MathNet.
        ///
        /// GitHub info:
        ///     mathnet/mathnet-numerics
        ///     https://github.com/mathnet/mathnet-numerics
        ///     MIT/X11
        /// </summary>
        private static class DoubleHelper
        {
            private const double NegativeMachineEpsilon = 1.1102230246251565e-16D;
            private const double PositiveMachineEpsilon = 2D * NegativeMachineEpsilon;

            public static bool IsZero(double value)
            {
                return !(value > PositiveMachineEpsilon) && !(value < -PositiveMachineEpsilon);
            }
        }
    }
}