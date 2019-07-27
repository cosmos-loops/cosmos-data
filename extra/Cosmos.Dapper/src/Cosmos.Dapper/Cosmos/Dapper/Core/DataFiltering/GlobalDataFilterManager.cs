using System;
using System.Collections.Concurrent;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Core.DataFiltering
{
    public static class GlobalDataFilterManager
    {
        private static readonly ConcurrentDictionary<(Type, Type), ISQLPredicate> _sqlPredicateCache;

        static GlobalDataFilterManager()
        {
            _sqlPredicateCache = new ConcurrentDictionary<(Type, Type), ISQLPredicate>();
        }

        public static void Register((Type, Type) key, ISQLPredicate predicate)
        {
            _sqlPredicateCache.AddOrUpdate(key, predicate, (tuple, sqlPredicate) => predicate);
        }

        public static void Register<TEntity>(GlobalLevelDataFilteringStrategy<TEntity> strategy) where TEntity : class, IEntity
        {
            if (strategy == null)
                return;

            Register(strategy.GetSignature(), strategy.GetFilteringPredicate());
        }

        public static ISQLPredicate GetFilter((Type, Type) key)
        {
            return _sqlPredicateCache.TryGetValue(key, out var ret) ? ret : null;
        }
    }
}