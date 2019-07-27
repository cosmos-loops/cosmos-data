using System;
using Cosmos.Dapper.Conventions;
using Dapper;
using Dapper.FluentMap;
using Dapper.Mapper;

namespace Cosmos.Dapper
{
    public class FluentMapConfiguration : IFluentDapperMappingConfig
    {
        private readonly IDapperMappingConfig _mappingConfig;
        private readonly IInternalDapperMappingConfig _internalMappingConfig;

        public FluentMapConfiguration(IDapperMappingConfig mappingConfig)
        {
            _mappingConfig = mappingConfig;
            _internalMappingConfig = mappingConfig as IInternalDapperMappingConfig;
        }

        public IFluentDapperMappingConfig AddMap<TEntity>(ClassMap<TEntity> classMap) where TEntity : class
        {
            _internalMappingConfig.SetMap(classMap);
            return this;
        }

        public IFluentDapperMappingConfig AddConvention<TConvention>(Action<FluentConventionConfiguration> configureConvention)
            where TConvention : ConventionBase, new()
        {
            var conventionConfig = new FluentConventionConfiguration(new TConvention(), _mappingConfig);
            configureConvention?.Invoke(conventionConfig);
            return this;
        }

        public IFluentDapperMappingConfig ConfigureOptions(Action<DapperOptions> configure)
        {
            configure?.Invoke(_mappingConfig.Options);
            return this;
        }
    }
}