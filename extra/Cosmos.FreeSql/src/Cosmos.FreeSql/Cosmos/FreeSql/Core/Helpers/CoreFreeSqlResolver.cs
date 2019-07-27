using System;
using Cosmos.Data.Context;

namespace Cosmos.FreeSql.Core.Helpers
{
    public static class CoreFreeSqlResolver
    {
        private static IFreeSqlGetter _getter;

        public static void Initialize(IFreeSqlGetter getter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public static IFreeSql Resolve(string connectionString) => _getter.Resolve(connectionString);

        public static IFreeSql Resolve(int hash) => _getter.Resolve(hash);

        public static IFreeSql<TDbContext> Resolve<TDbContext>(string connectionString) where TDbContext : class, IDbContext => _getter.Resolve<TDbContext>(connectionString);

        public static IFreeSql<TDbContext> Resolve<TDbContext>(int hash) where TDbContext : class, IDbContext => _getter.Resolve<TDbContext>(hash);

        public static IFreeSql RequiredResolve(string connectionString) => _getter.RequiredResolve(connectionString);

        public static IFreeSql RequiredResolve(int hash) => _getter.RequiredResolve(hash);

        public static IFreeSql<TDbContext> RequiredResolve<TDbContext>(string connectionString) where TDbContext : class, IDbContext => _getter.RequiredResolve<TDbContext>(connectionString);

        public static IFreeSql<TDbContext> RequiredResolve<TDbContext>(int hash) where TDbContext : class, IDbContext => _getter.RequiredResolve<TDbContext>(hash);
    }
}