using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Domain.Core;
using Dapper.Mapper;

namespace Cosmos.Dapper.EntityMapping
{
    public class DapperEntityTypeBuilder<TEntity> where TEntity : class, IEntity
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly DapperConfig _mappingConfig;

        public DapperEntityTypeBuilder(DapperConfig config)
        {
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
            ClassMapper = _mappingConfig.GetInternalMap<TEntity>();
        }

        private IInternalClassMapper<TEntity> ClassMapper { get; }

        public DapperEntityTypeBuilder<TEntity> ToSchema(string schemaName)
        {
            ClassMapper.InternalSchema(schemaName);
            return this;
        }

        public DapperEntityTypeBuilder<TEntity> ToTable(string tableName)
        {
            ClassMapper.InternalTable(tableName);
            return this;
        }

        public PropertyMap ForProperty<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var property = LambdaHelper.GetProperty(expression);
            return ClassMapper.InternalGetPropertyMap(property);
        }
    }
}