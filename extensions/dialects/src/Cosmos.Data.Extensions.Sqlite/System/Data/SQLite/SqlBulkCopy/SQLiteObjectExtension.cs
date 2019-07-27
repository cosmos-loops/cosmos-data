/*
 * Reference to:
 *     herryh/SQLiteBulkCopy
 *     Author: Herry Hamidjaja
 *     Url: https://github.com/herryh/SQLiteBulkCopy
 *     MIT
 */

namespace System.Data.SQLite.SqlBulkCopy
{
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
            if (a.GetType().Name == "Byte[]")
            {
                return BitConverter.ToString(a as byte[]).Replace("-", "");
            }

            return a.ToString();
        }
    }
}