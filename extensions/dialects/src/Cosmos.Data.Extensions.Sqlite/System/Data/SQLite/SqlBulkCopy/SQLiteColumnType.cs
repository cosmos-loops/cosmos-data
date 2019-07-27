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
        //
        // Summary:
        //     A signed integer.
        Integer = 1,

        //
        // Summary:
        //     A floating point value.
        Real = 2,

        //
        // Summary:
        //     A text string.
        Text = 3,

        //
        // Summary:
        //     A blob of data.
        Blob = 4,

        // Summary:
        //     A date column
        Date = 5,

        // Summary:
        //     A numeric column
        Numeric = 6,

        // Summary
        //     A boolean column
        Boolean = 7
    }
}