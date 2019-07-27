using Cosmos.Domain.Core;
using Cosmos.Domain.EntityDescriptors;

namespace Cosmos.FreeSql.Map
{
    public abstract class AggregateRootMap<TEntity> : VersionableRootMapBase<TEntity>, IOracleEntityMap
        where TEntity : class, IEntity, IVersionable, new()
    {
        /// <summary>
        /// 映射乐观离线锁
        /// </summary>
        protected override void MapVersion(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(t => t.Version).MakeVersionable();
        }
    }
}