using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    public static class SQLPredicateMerger
    {
        public static ISQLPredicate Merge(ISQLPredicate[] predicates)
        {
            return Merge(null, predicates);
        }
        
        public static ISQLPredicate Merge(ISQLPredicate where, params ISQLPredicate[] filters)
        {
            if (filters == null || !filters.Any())
                return where;

            var group = new SQLPredicateGroup
            {
                Operator = SQLGroupOperator.AND,
                Predicates = new List<ISQLPredicate>()
            };

            if (where != null)
                group.Predicates.Add(where);

            foreach (var filter in filters)
            {
                if (filter == null)
                    continue;

                group.Predicates.Add(filter);
            }

            return group;
        }

        public static ISQLPredicate Join(this ISQLPredicate where, params ISQLPredicate[] filters)
        {
            return Merge(where, filters);
        }
    }
}