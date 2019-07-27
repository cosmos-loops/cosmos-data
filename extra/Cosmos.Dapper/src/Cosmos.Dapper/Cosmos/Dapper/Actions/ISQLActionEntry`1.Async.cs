using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionAsyncEntry<TEntity> : ISQLActionAsyncEntry where TEntity : class, IEntity, new()
    {
        AsynchronousInsertAction<TEntity> Insert(TEntity entity);

        AsynchronousUpdateAction<TEntity> Update(TEntity entity, bool ignoreAllKeyProperties = false);

        AsynchronousDeleteAction<TEntity> Delete(TEntity entity);

        AsynchronousBatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities);

        AsynchronousBatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false);

        AsynchronousBatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities);
    }
}