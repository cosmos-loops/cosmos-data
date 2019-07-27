using Cosmos.Domain.Core;

namespace Cosmos.FreeSql.Map
{
    public abstract class EntityMap<TEntity> : MapBase<TEntity>, ISqlServerEntityMap where TEntity : class, IEntity, new() { }
}