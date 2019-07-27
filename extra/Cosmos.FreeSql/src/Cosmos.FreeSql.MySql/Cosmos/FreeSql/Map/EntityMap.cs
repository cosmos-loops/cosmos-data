using Cosmos.Domain.Core;

namespace Cosmos.FreeSql.Map
{
    public abstract class EntityMap<TEntity> : MapBase<TEntity>, IMySqlEntityMap where TEntity : class, IEntity, new() { }
}