using System;
using System.Linq.Expressions;
using Cosmos.Domain.Core;
using FreeSql;
using FreeSql.DataAnnotations;

namespace Cosmos.FreeSql.Map
{
    public class EntityTypeBuilder<TEntity> where TEntity : class, IEntity, new()
    {
        private ICodeFirst ModelBuilder { get; }
        private Func<Action<TableFluent<TEntity>>, ICodeFirst> EntityConfigurePointer { get; }

        public EntityTypeBuilder(ICodeFirst cf)
        {
            ModelBuilder = cf ?? throw new ArgumentNullException(nameof(cf));
            EntityConfigurePointer = t => ModelBuilder.ConfigEntity(t);
        }

        public virtual EntityTypeBuilder<TEntity> ToTable(string tableName)
        {
            EntityConfigurePointer(entity => entity.Name(tableName));
            return this;
        }

        public virtual EntityTypeBuilder<TEntity> HasQueryFilter(string filterCondition)
        {
            EntityConfigurePointer(entity => entity.SelectFilter(filterCondition));
            return this;
        }
        
        public EntityColumnTypeBuilder<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            var tableAttr = ModelBuilder.GetConfigEntity(typeof(TEntity));
            var column = new TableFluent<TEntity>(tableAttr).Property(propertyExpression);
            return new EntityColumnTypeBuilder<TEntity, TProperty>(ModelBuilder, this, column);
        }
    }
}