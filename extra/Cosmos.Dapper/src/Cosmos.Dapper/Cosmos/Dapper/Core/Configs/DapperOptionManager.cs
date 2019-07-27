namespace Cosmos.Dapper.Core.Configs
{
    public static class DapperOptionManager
    {
        private static readonly DapperOptionCollection _collectionCache = new DapperOptionCollection();

        public static DapperOptions Get(string name) => _collectionCache.GetOptions(name);

        public static DapperOptions<TContext> Get<TContext>() where TContext : class, IDapperContext => _collectionCache.GetOptions<TContext>();

        public static DapperOptions<TContext> Get<TContext>(string name) where TContext : class, IDapperContext => _collectionCache.GetOptions<TContext>(name);

        public static void Set(DapperOptions options) => _collectionCache.Add(options.Name, options);

        public static void Set<TContext>(string name, DapperOptions<TContext> options) where TContext : class, IDapperContext => _collectionCache.Add(name, options);
    }
}