using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Data.SqlKata;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public class SQLPredicateGroup : ISQLPredicateGroup
    {
        public SQLGroupOperator Operator { get; set; }

        public IList<ISQLPredicate> Predicates { get; set; }

        public string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var seperator = Operator == SQLGroupOperator.AND ? " AND " : " OR ";
            return "(" +
                   Predicates.Aggregate(
                       new StringBuilder(),
                       (sb, p) => (sb.Length == 0 ? sb : sb.Append(seperator)).Append(p.GetSql(sqlGenerator, parameters)),
                       sb =>
                       {
                           var s = sb.ToString();
                           return s.Length == 0 ? sqlGenerator.Configuration.Dialect.EmptyExpression : s;
                       }
                   ) +
                   ")";
        }
    }
}
