using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetCore.Collections.Paginable;
using FreeSql;

namespace Cosmos.FreeSql.Store
{
    public abstract partial class StoreBase<TEntity, TKey>
    {
        public virtual ISelect<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate);
        }

        public virtual List<TEntity> Find<TMenber>(
            Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize,
            Expression<Func<TEntity, TMenber>> columnCondition = null, bool asc = true,
            bool includeNestedMembers = false)
        {
            var select = Set.Where(predicate).Page(pageNumber, pageSize);
            if (columnCondition != null)
            {
                select = asc
                    ? select.OrderBy(columnCondition)
                    : select.OrderByDescending(columnCondition);
            }

            return select.ToList(includeNestedMembers);
        }

        public virtual TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).First();
        }

        public virtual TEntity FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).ToOne();
        }

        public IPage<TEntity> QueryPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, bool includeNestedMembers = false)
        {
            return Set.Where(predicate).GetPage(pageNumber, pageSize, includeNestedMembers);
        }

        public virtual Task<List<TEntity>> FindAsync<TMenber>(
            Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize,
            Expression<Func<TEntity, TMenber>> columnCondition = null, bool asc = true,
            bool includeNestedMembers = false)
        {
            var select = Set.Where(predicate).Page(pageNumber, pageSize);
            if (columnCondition != null)
            {
                select = asc
                    ? select.OrderBy(columnCondition)
                    : select.OrderByDescending(columnCondition);
            }

            return select.ToListAsync(includeNestedMembers);
        }

        public virtual Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).FirstAsync();
        }

        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).ToOneAsync();
        }

        public Task<IPage<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, bool includeNestedMembers = false)
        {
            return Set.Where(predicate).GetPageAsync(pageNumber, pageSize, includeNestedMembers);
        }
    }
}