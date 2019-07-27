using Cosmos.Dapper.Actions;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        public ISQLActionEntry ActionEntry => RawTypedContext.GetActionEntry(RepoLevelDataFilters);
        public ISQLActionAsyncEntry AsynchronousActionEntry => RawTypedContext.GetAsynchronousActionEntry(RepoLevelDataFilters);
    }
}