using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Cosmos.Data.Context;

namespace Cosmos.Data.Core
{
    public static class SystemSupportRegistrar
    {
        private static readonly ConcurrentDictionary<string, Action<IDbContextConfig>> _actions;
        private static readonly HashSet<string> _check;

        static SystemSupportRegistrar()
        {
            _actions = new ConcurrentDictionary<string, Action<IDbContextConfig>>();
            _check = new HashSet<string>();
        }

        public static void AddDescriptorOnce(string key, Action<IDbContextConfig> config)
        {
            if (_check.Contains(key) || _actions.ContainsKey(key))
                return;
            _check.Add(key);
            _actions.TryAdd(key, config);
        }

        public static IEnumerable<Action<IDbContextConfig>> GetActionsOnce()
        {
            var ret = _actions.Values;
            _actions.Clear();
            return ret;
        }
    }
}