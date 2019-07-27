using System;
using Cosmos.Disposables;
using Cosmos.Domain.Core;
using FreeSql;

namespace Cosmos.FreeSql.Store
{
    public abstract partial class StoreBase<TEntity, TKey> :
        DisposableObjects
        where TEntity : class, IEntity<TKey>, new()
    {
        protected StoreBase(DbContextBase context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected StoreBase(DbContextBase context, bool includeUnsafeOpt)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            IncludeUnsafeOpt = includeUnsafeOpt;
        }

        protected bool IncludeUnsafeOpt { get; }

        protected DbContextBase Context { get; }

        protected DbSet<TEntity> Set => Context.Set<TEntity>();
    }
}