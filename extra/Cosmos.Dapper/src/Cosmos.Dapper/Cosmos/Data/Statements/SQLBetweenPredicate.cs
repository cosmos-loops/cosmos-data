using System.Collections.Generic;
using Dapper.Internals;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public class SQLBetweenPredicate<T> : SQLBasePredicate, ISQLBetweenPredicate where T : class
    {
        public override string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);
            var propertyName1 = parameters.SetParameterName(PropertyName, Value.Value1, sqlGenerator.Configuration.Dialect.ParameterPrefix);
            var propertyName2 = parameters.SetParameterName(PropertyName, Value.Value2, sqlGenerator.Configuration.Dialect.ParameterPrefix);
            return $"({columnName} {(Not ? "NOT " : string.Empty)}BETWEEN {propertyName1} AND {propertyName2})";
        }

        public SQLBetweenValues Value { get; set; }

        public bool Not { get; set; }
    }
}