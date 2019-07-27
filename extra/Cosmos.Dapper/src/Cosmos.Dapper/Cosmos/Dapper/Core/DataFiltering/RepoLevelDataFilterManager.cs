using System;
using System.Collections.Concurrent;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DataFiltering
{
    public static class RepoLevelDataFilterManager
    {
        private static readonly ConcurrentDictionary<(Type, Type), ISQLPredicate> _sqlPredicateCache;
        private static object _lockObj = new object();

        static RepoLevelDataFilterManager()
        {
            _sqlPredicateCache = new ConcurrentDictionary<(Type, Type), ISQLPredicate>();
        }

        public static bool IsContainerKey((Type, Type) key)
        {
            return _sqlPredicateCache.ContainsKey(key);
        }

        public static void Register((Type, Type) key, ISQLPredicate predicate)
        {
            lock (_lockObj)
            {
                if (!IsContainerKey(key))
                {
                    _sqlPredicateCache.AddOrUpdate(key, predicate, (tuple, sqlPredicate) => predicate);
                }
            }
        }

        public static ISQLPredicate GetFilter((Type, Type) key)
        {
            return _sqlPredicateCache.TryGetValue(key, out var ret) ? ret : null;
        }
    }
}