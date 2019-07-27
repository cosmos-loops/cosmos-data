using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper.Mapper;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public class SQLSortSet : List<SQLSort>
    {
        public override string ToString()
        {
            Sort(new SQLSortComparer());
            var sb = new StringBuilder();

            for (var i = 0; i < Count; i++)
            {
                sb.AppendFormat("{0} {1}", this[i].FieldName, this[i].SortType);

                if (i < Count - 1)
                    sb.Append(", ");
            }

            return sb.ToString();
        }

        public IEnumerable<string> ToStrings(IClassMap classMap, Func<IClassMap, string, bool, string> columnNameFunc)
        {
            return this.Select(s => $"{columnNameFunc(classMap, s.FieldName, false)} {s.SortType}");
        }
    }
}