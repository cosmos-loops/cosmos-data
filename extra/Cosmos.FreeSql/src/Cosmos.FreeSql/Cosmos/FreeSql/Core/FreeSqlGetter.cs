using System;
using Cosmos.Data.Context;

namespace Cosmos.FreeSql.Core
{
    public class FreeSqlGetter : IFreeSqlGetter
    {
        private readonly FreeSqlManager _manager;

        public FreeSqlGetter(FreeSqlManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public IFreeSql Resolve(string connectionString)
        {
            return _manager.Get(connectionString);
        }

        public IFreeSql<TDbContext> Resolve<TDbContext>(string connectionString) where TDbContext : class, IDbContext
        {
            return _manager.Get(connectionString) as IFreeSql<TDbContext>;
        }

        public IFreeSql Resolve(int hash)
        {
            return _manager.Get(hash);
        }

        public IFreeSql<TDbContext> Resolve<TDbContext>(int hash) where TDbContext : class, IDbContext
        {
            return _manager.Get(hash) as IFreeSql<TDbContext>;
        }

        public IFreeSql RequiredResolve(string connectionString)
        {
            var ret = _manager.Get(connectionString);
            if (ret == null)
                throw new ArgumentException($"There's no instance for '{connectionString}'.");
            return ret;
        }

        public IFreeSql<TDbContext> RequiredResolve<TDbContext>(string connectionString) where TDbContext : class, IDbContext
        {
            var ret = _manager.Get(connectionString);
            if (ret == null)
                throw new ArgumentException($"There's no instance for '{connectionString}'.");
            return ret as IFreeSql<TDbContext>;
        }

        public IFreeSql RequiredResolve(int hash)
        {
            var ret = _manager.Get(hash);
            if (ret == null)
                throw new ArgumentException($"There's no instance for '{hash}'.");
            return ret;
        }

        public IFreeSql<TDbContext> RequiredResolve<TDbContext>(int hash) where TDbContext : class, IDbContext
        {
            var ret = _manager.Get(hash);
            if (ret == null)
                throw new ArgumentException($"There's no instance for '{hash}'.");
            return ret as IFreeSql<TDbContext>;
        }
    }
}