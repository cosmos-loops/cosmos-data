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
    /// Defines the mapping between a column in a SqliteBulkCopy instance's data source and a column in the instance's destination table.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public enum SQLiteColumnType
    {
        /// <summary>
        /// A signed integer.
        /// </summary>
        Integer = 1,

        /// <summary>
        /// A floating point value.
        /// </summary>
        Real = 2,

        /// <summary>
        /// A text string.
        /// </summary>
        Text = 3,

        /// <summary>
        /// A blob of data.
        /// </summary>
        Blob = 4,

        /// <summary>
        /// A date column
        /// </summary>
        Date = 5,

        /// <summary>
        /// A numeric column
        /// </summary>
        Numeric = 6,

        /// <summary>
        /// A boolean column
        /// </summary>
        Boolean = 7
    }
}