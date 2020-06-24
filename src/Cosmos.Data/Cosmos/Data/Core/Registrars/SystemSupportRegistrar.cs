using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Cosmos.Data.Core.Registrars
{
    /// <summary>
    /// System level support register
    /// </summary>
    public static class SystemSupportRegistrar
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ConcurrentDictionary<string, Action<IDbContextConfig>> _actions;

        // ReSharper disable once InconsistentNaming
        private static readonly HashSet<string> _check;

        static SystemSupportRegistrar()
        {
            _actions = new ConcurrentDictionary<string, Action<IDbContextConfig>>();
            _check = new HashSet<string>();
        }

        /// <summary>
        /// Add description once
        /// </summary>
        /// <param name="key"></param>
        /// <param name="config"></param>
        public static void AddDescriptorOnce(string key, Action<IDbContextConfig> config)
        {
            if (_check.Contains(key) || _actions.ContainsKey(key))
                return;

            _check.Add(key);
            _actions.TryAdd(key, config);
        }

        /// <summary>
        /// Gets actions once
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Action<IDbContextConfig>> GetActionsOnce()
        {
            var ret = _actions.Values;
            _actions.Clear();
            return ret;
        }
    }
}