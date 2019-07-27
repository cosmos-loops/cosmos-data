using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Store;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Store
{
    public abstract class RepositoryBase<TContext, TEntity> : StoreBase<TContext, TEntity>, IRepository
        where TContext : class, IDapperContext, IStoreContext, IWithSQLGenerator
        where TEntity : class, IEntity, new()
    {
        protected RepositoryBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt = true)
            : base(context, bindingExpression, includeUnsafeOpt) { }

        public virtual void ScopedInitialize(IStoreContextManager manager)
        {
            manager.Register(typeof(TContext), RawTypedContext);
        }
    }
}