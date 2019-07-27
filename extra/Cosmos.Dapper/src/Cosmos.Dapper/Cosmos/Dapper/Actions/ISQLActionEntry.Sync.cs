using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionEntry
    {
        InsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        UpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        DeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        BatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();

        BatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        BatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();
    }
}