using Cosmos.Dapper.EntityMapping;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Map
{
    public abstract class EntityMap<TEntity> : DapperMapBase<TEntity>, IPostgreSqlEntityMap where TEntity : class, IEntity, new() { }
}