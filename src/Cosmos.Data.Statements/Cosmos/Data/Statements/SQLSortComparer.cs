using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace Cosmos.Data.Statements
{
    public class SQLSortComparer : IComparer<SQLSort>
    {
        public int Compare(SQLSort x, SQLSort y)
        {
            return x?.Index ?? 0 - y?.Index ?? 0;
        }
    }
}
