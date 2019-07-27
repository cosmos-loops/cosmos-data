using System.Collections.Concurrent;

namespace Cosmos.Dapper.Core.Configs
{
    public static class DapperConfigAccessor
    {
        private static readonly ConcurrentDictionary<int, DapperConfig> _dapperConfigCache = new ConcurrentDictionary<int, DapperConfig>();
        private static DapperConfig DapperMappingConfig { get; set; }

        public static DapperConfig Cache(string key)
        {
            return key == null
                ? null
                : _dapperConfigCache.TryGetValue(key.GetHashCode(), out var ret)
                    ? ret
                    : null;
        }

        public static void RefreshCache(string key, DapperConfig config)
        {
            if (key == null)
                return;

            _dapperConfigCache.AddOrUpdate(key.GetHashCode(), config, (h, c) => config);
        }
    }
}