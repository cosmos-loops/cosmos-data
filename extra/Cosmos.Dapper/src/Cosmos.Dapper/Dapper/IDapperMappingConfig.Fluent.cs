using System;
using Cosmos.Dapper;
using Cosmos.Dapper.Conventions;
using Dapper.FluentMap;
using Dapper.Mapper;

namespace Dapper
{
    public interface IFluentDapperMappingConfig
    {
        IFluentDapperMappingConfig AddMap<TEntity>(ClassMap<TEntity> classMap) where TEntity : class;

        IFluentDapperMappingConfig AddConvention<TConvention>(Action<FluentConventionConfiguration> configureConvention)
            where TConvention : ConventionBase, new();

        IFluentDapperMappingConfig ConfigureOptions(Action<DapperOptions> configure);
    }
}