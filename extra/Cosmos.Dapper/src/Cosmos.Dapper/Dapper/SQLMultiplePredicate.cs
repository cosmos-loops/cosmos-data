using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Data.Statements;

// ReSharper disable InconsistentNaming

namespace Dapper
{
    public class SQLMultiplePredicate
    {
        private readonly List<SQLMultiplePredicateItem> _items;

        public SQLMultiplePredicate()
        {
            _items = new List<SQLMultiplePredicateItem>();
        }

        public IEnumerable<SQLMultiplePredicateItem> Items => _items.AsReadOnly();

        public void Add<T>(ISQLPredicate predicate, SQLSortSet sort = null) where T : class
        {
            _items.Add(new SQLMultiplePredicateItem
            {
                Value = predicate,
                Type = typeof(T),
                SortSet = sort
            });
        }

        public void Add<T>(object id) where T : class
        {
            _items.Add(new SQLMultiplePredicateItem
            {
                Value = id,
                Type = typeof(T)
            });
        }

        public class SQLMultiplePredicateItem
        {
            public object Value { get; set; }

            public Type Type { get; set; }

            public SQLSortSet SortSet { get; set; }
        }
    }
}
