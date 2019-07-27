using Cosmos.Domain.Core;
using FreeSql;

namespace Cosmos.FreeSql.Map
{
    public abstract class MapBase<TEntity> : IEntityMap where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Module builder
        /// </summary>
        protected EntityTypeBuilder<TEntity> ModelBuilder { get; private set; }

        public void Map(ICodeFirst cf)
        {
            ModelBuilder = new EntityTypeBuilder<TEntity>(cf);

            MapTable(ModelBuilder);
            MapVersion(ModelBuilder);
            MapProperties(ModelBuilder);
            MapAssociations(ModelBuilder);
        }

        /// <summary>
        /// Mapping table
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapTable(EntityTypeBuilder<TEntity> builder) { }

        /// <summary>
        /// Mapping Optimistic Lock
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapVersion(EntityTypeBuilder<TEntity> builder) { }

        /// <summary>
        /// Mapping properties
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapProperties(EntityTypeBuilder<TEntity> builder) { }

        /// <summary>
        /// Mapping associations
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapAssociations(EntityTypeBuilder<TEntity> builder) { }
    }
}