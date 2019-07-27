using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Domain.EntityDescriptors;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        public virtual bool RemoveById(dynamic id)
        {
            return !(FindById(id) is TEntity entity) ||
                   RawTypedContext.EntityOperators.Delete(entity, RepoLevelDataFilters);
        }

        public virtual async Task<bool> RemoveByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            return !(await FindByIdAsync(id, cancellationToken) is TEntity entity) ||
                   await RawTypedContext.EntityOperators.DeleteAsync(entity, RepoLevelDataFilters, cancellationToken);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (entity is IDeletable deletableEntity)
            {
                deletableEntity.IsDeleted = true;
                RawTypedContext.EntityOperators.Update(entity, RepoLevelDataFilters);
            }
            else if (IncludeUnsafeOpt)
            {
                RawTypedContext.EntityOperators.Delete(entity, RepoLevelDataFilters);
            }

        }

        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            var list = entities.ToList();
            if (!list.Any())
                return;
            // ReSharper disable once SuspiciousTypeConversion.Global
            // ReSharper disable once UnusedVariable
            if (list[0] is IDeletable deletableEntity)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                foreach (IDeletable entity in list)
                {
                    entity.IsDeleted = true;
                }

                RawTypedContext.EntityOperators.Update<TEntity>(list, RepoLevelDataFilters);
            }
            else if (IncludeUnsafeOpt)
            {
                RawTypedContext.EntityOperators.Delete<TEntity>(list, RepoLevelDataFilters);
            }
        }

        public virtual bool Remove(object predicate)
        {
            return RawTypedContext.EntityOperators.Delete<TEntity>(predicate, RepoLevelDataFilters);
        }

        public virtual bool Remove(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.Delete(predicate, RepoLevelDataFilters);
        }

        public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (entity is IDeletable deletableEntity)
            {
                deletableEntity.IsDeleted = true;
                await RawTypedContext.EntityOperators.UpdateAsync(entity, RepoLevelDataFilters, cancellationToken: cancellationToken);
            }
            else if (IncludeUnsafeOpt)
            {
                await RawTypedContext.EntityOperators.DeleteAsync(entity, RepoLevelDataFilters, cancellationToken);
            }
        }

        public virtual async Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            var list = entities.ToList();
            if (!list.Any())
                return;
            // ReSharper disable once SuspiciousTypeConversion.Global
            // ReSharper disable once UnusedVariable
            if (list[0] is IDeletable deletableEntity)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                foreach (IDeletable entity in list)
                {
                    entity.IsDeleted = true;
                }

                await RawTypedContext.EntityOperators.UpdateAsync<TEntity>(list, RepoLevelDataFilters, cancellationToken: cancellationToken);
            }
            else if (IncludeUnsafeOpt)
            {
                await RawTypedContext.EntityOperators.DeleteAsync<TEntity>(list, RepoLevelDataFilters, cancellationToken);
            }
        }

        public Task<bool> RemoveAsync(object predicate, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.DeleteAsync<TEntity>(predicate, RepoLevelDataFilters, cancellationToken);
        }

        public Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.DeleteAsync(predicate, RepoLevelDataFilters, cancellationToken);
        }
    }
}