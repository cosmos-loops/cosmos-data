using System.Collections.Generic;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public class SQLPropertyPredicate<T1, T2> : SQLComparePredicate, ISQLPropertyPredicate
    where T1 : class where T2 : class
    {
        public string PropertyName2 { get; set; }

        public override string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T1), sqlGenerator, PropertyName);
            var columnName2 = GetColumnName(typeof(T2), sqlGenerator, PropertyName2);
            return $"({columnName} {GetOperatorString()} {columnName2})";
        }
    }
}
