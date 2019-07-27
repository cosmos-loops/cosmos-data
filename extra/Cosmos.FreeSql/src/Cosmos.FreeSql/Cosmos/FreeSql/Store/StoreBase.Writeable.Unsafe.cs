using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cosmos.FreeSql.Store
{
    public abstract partial class StoreBase<TEntity, TKey>
    {
        public virtual void UnsafeRemove(TEntity entity)
        {
            if (entity == null)
                return;
            InternalDelete(entity, true);
        }

        public virtual void UnsafeRemove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            InternalDelete(entities.ToList(), true);
        }

        public virtual void UnsafeRemove(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return;
            var entities = Find(predicate).ToList();
            InternalDelete(entities, true);
        }

        public virtual async Task UnsafeRemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return;
            var entities = await Find(predicate).ToListAsync();
            InternalDelete(entities, true);
        }
    }
}