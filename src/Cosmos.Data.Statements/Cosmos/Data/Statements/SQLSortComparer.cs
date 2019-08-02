using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace Cosmos.Data.Statements
{
    /// <summary>
    /// SQL Sort Comparer
    /// </summary>
    public class SQLSortComparer : IComparer<SQLSort>
    {
        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(SQLSort x, SQLSort y)
        {
            return x?.Index ?? 0 - y?.Index ?? 0;
        }
    }
}