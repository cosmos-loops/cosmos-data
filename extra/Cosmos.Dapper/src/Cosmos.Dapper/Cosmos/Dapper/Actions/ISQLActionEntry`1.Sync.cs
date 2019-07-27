using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionEntry<TEntity> : ISQLActionEntry where TEntity : class, IEntity, new()
    {
        InsertAction<TEntity> Insert(TEntity entity);

        UpdateAction<TEntity> Update(TEntity entity,bool ignoreAllKeyProperties =false);

        DeleteAction<TEntity> Delete(TEntity entity);

        BatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities);

        BatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities,bool ignoreAllKeyProperties =false);

        BatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities);
    }
}