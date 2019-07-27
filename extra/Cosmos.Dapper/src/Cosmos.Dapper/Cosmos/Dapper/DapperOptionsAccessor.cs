using Cosmos.Dapper.Core.Configs;

namespace Cosmos.Dapper
{
    public class DapperOptionsAccessor
    {
        public DapperOptions Get(string name) => DapperOptionManager.Get(name);
        public DapperOptions<TContext> Get<TContext>() where TContext : class, IDapperContext => DapperOptionManager.Get<TContext>();
        public DapperOptions<TContext> Get<TContext>(string name) where TContext : class, IDapperContext => DapperOptionManager.Get<TContext>(name);
    }
}