using Cosmos.Data.Context;

namespace Cosmos.FreeSql
{
    public interface IFreeSqlGetter
    {
        IFreeSql Resolve(string connectionString);

        IFreeSql Resolve(int hash);

        IFreeSql<TDbContext> Resolve<TDbContext>(string connectionString) where TDbContext : class, IDbContext;

        IFreeSql<TDbContext> Resolve<TDbContext>(int hash) where TDbContext : class, IDbContext;

        IFreeSql RequiredResolve(string connectionString);

        IFreeSql RequiredResolve(int hash);

        IFreeSql<TDbContext> RequiredResolve<TDbContext>(string connectionString) where TDbContext : class, IDbContext;

        IFreeSql<TDbContext> RequiredResolve<TDbContext>(int hash) where TDbContext : class, IDbContext;
    }
}