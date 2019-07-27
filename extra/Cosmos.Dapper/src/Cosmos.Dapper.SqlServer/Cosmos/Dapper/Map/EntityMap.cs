using Cosmos.Dapper.EntityMapping;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Map
{
    public abstract class EntityMap<TEntity> : DapperMapBase<TEntity>, ISqlServerEntityMap where TEntity : class, IEntity, new() { }
}