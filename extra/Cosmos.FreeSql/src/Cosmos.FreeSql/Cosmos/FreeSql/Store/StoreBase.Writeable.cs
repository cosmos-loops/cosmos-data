using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cosmos.Domain.EntityDescriptors;

namespace Cosmos.FreeSql.Store
{
    public abstract partial class StoreBase<TEntity, TKey>
    {
        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Set.Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            Set.AddRange(entities);
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Set.AddOrUpdate(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Set.AddAsync(entity);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            await Set.AddRangeAsync(entities);
        }

        public virtual async Task AddOrUpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Set.AddOrUpdateAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Set.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            Set.UpdateRange(entities);
        }

        public virtual int Update(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return 0;
            return Context.Orm.Update<TEntity>().Where(predicate).ExecuteAffrows();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Set.UpdateAsync(entity);
        }

        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            await Set.UpdateRangeAsync(entities);
        }

        public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return 0;
            return await Context.Orm.Update<TEntity>().Where(predicate).ExecuteAffrowsAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
                return;
            InternalDelete(entity, IncludeUnsafeOpt);
        }

        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            InternalDelete(entities.ToList(), IncludeUnsafeOpt);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return;
            var entities = Find(predicate).ToList();
            InternalDelete(entities, IncludeUnsafeOpt);
        }

        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return;
            var entities = await Find(predicate).ToListAsync();
            InternalDelete(entities, IncludeUnsafeOpt);
        }

        private void InternalDelete(TEntity entity, bool includeUnsafeOpt = false)
        {
            if (entity == null)
                return;
            if (entity is IDeletable model)
            {
                model.IsDeleted = true;
                Set.Update(entity);
            }
            else if (includeUnsafeOpt)
            {
                Set.Remove(entity);
            }
        }

        private void InternalDelete(List<TEntity> entities, bool includeUnsafeOpt = false)
        {
            if (entities == null)
                return;
            if (!entities.Any())
                return;
            if (entities[0] is IDeletable)
            {
                foreach (var entity in entities.Select(t => (IDeletable) t))
                    entity.IsDeleted = true;
                Set.UpdateRange(entities);
            }
            else if (includeUnsafeOpt)
            {
                Set.RemoveRange(entities);
            }
        }
    }
}