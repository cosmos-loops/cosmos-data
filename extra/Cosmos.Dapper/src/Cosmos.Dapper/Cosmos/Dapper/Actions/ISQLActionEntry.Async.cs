using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionAsyncEntry
    {
        AsynchronousInsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        AsynchronousUpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        AsynchronousDeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        AsynchronousBatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();

        AsynchronousBatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        AsynchronousBatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();
    }
}