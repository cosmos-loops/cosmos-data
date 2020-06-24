using System;
using System.Collections.Concurrent;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Uow-Ref holding strategies manager
    /// </summary>
    public static class UowRefStrategyFunctions
    {
        /// <summary>
        /// Default strategy name
        /// </summary>
        public const string DefaultName = "DefaultUowRefStrategy";

        private static ConcurrentDictionary<string, Func<IUowRefStrategy>> _strategies;
        private static Func<IUowRefStrategy> _defaultStrategy;
        private static readonly Func<IUowRefStrategy> _systemLevelDefaultStrategy;

        static UowRefStrategyFunctions()
        {
            _defaultStrategy = () => new DefaultUowRefStrategy();
            _systemLevelDefaultStrategy = () => new DefaultUowRefStrategy();
            _strategies = new ConcurrentDictionary<string, Func<IUowRefStrategy>>
            {
                [DefaultName] = _defaultStrategy
            };
        }

        /// <summary>
        /// To override strategy
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strategy"></param>
        /// <param name="asDefault"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void OverrideStrategy(string name, Func<IUowRefStrategy> strategy, bool asDefault = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (strategy is null)
                throw new ArgumentNullException(nameof(strategy));
            _strategies.AddOrUpdate(name, n => strategy, (n, s) => strategy);
            if (asDefault)
                _defaultStrategy = strategy;
        }

        /// <summary>
        /// Get holding strategy.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Func<IUowRefStrategy> For(string name)
        {
            return _strategies.TryGetValue(name, out var strategy)
                ? strategy
                : _defaultStrategy ?? _systemLevelDefaultStrategy;
        }

        /// <summary>
        /// Get holding strategy.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="overrideStrategy"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Func<IUowRefStrategy> For(string name, Func<IUowRefStrategy> overrideStrategy)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (overrideStrategy is null)
                throw new ArgumentNullException(nameof(overrideStrategy));
            if (_strategies.TryGetValue(name, out var strategy))
                return strategy;
            OverrideStrategy(name, overrideStrategy);
            return overrideStrategy;
        }

        /// <summary>
        /// Gets default holding strategy.
        /// </summary>
        public static Func<IUowRefStrategy> Default => _defaultStrategy ?? _systemLevelDefaultStrategy;
    }
}