/*
 * Reference to:
 *     herryh/SQLiteBulkCopy
 *     Author: Herry Hamidjaja
 *     Url: https://github.com/herryh/SQLiteBulkCopy
 *     MIT
 */

namespace System.Data.SQLite.SqlBulkCopy
{
    /// <summary>
    /// Extensions fot Sqlite object
    /// </summary>
    // ReSharper disable once InconsistentNaming
    static class SQLiteObjectExtension
    {
        /// <summary>
        /// Provide Extension method to convert object to string and handle array of byte for varbinary.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static string ToSQLiteString(this object a)
        {
            if (a.GetType().Name == "Byte[]" && a is byte[] b)
            {
                return BitConverter.ToString(b).Replace("-", "");
            }

            return a.ToString();
        }
    }
}