using System;
using Cosmos.Domain.Core;
using FreeSql;
using FreeSql.DataAnnotations;

namespace Cosmos.FreeSql.Map
{
    public class EntityColumnTypeBuilder<TEntity, TProperty> where TEntity : class, IEntity, new()
    {
        private readonly EntityTypeBuilder<TEntity> _entityTypeBuilder;
        private ColumnFluent _columnFluentPointer;

        private ICodeFirst ModelBuilder { get; set; }

        internal EntityColumnTypeBuilder(ICodeFirst modelBuilder, EntityTypeBuilder<TEntity> entityTypeBuilder, ColumnFluent fluent)
        {
            _entityTypeBuilder = entityTypeBuilder;
            _columnFluentPointer = fluent ?? throw new ArgumentNullException(nameof(fluent));
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> MapTo(string columnName)
        {
            _columnFluentPointer.Name(columnName);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> AsDbType(string dbType)
        {
            _columnFluentPointer.DbType(dbType);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> AsPrimaryKey()
        {
            _columnFluentPointer.IsPrimary(true);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> AsIdentity()
        {
            _columnFluentPointer.IsIdentity(true);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> MakeVersionable()
        {
            _columnFluentPointer.IsVersion(true);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> AsNullableType()
        {
            _columnFluentPointer.IsNullable(true);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> MapToType(Type type)
        {
            _columnFluentPointer.MapType(type);
            return this;
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> MapToType<TTargetType>()
        {
            return MapToType(typeof(TTargetType));
        }

        public EntityColumnTypeBuilder<TEntity, TProperty> Ignore()
        {
            _columnFluentPointer.IsIgnore(true);
            return this;
        }
    }
}