using System.Collections.Generic;

namespace Cosmos.Dapper.Conventions
{
    public abstract class ConventionBase
    {
        private readonly List<ConventionConfig> _conventionConfigs = new List<ConventionConfig>();

        public List<ConventionConfig> ConventionConfigs => _conventionConfigs;

        protected ConventionConfig Properties()
        {
            var config = new ConventionConfig();
            _conventionConfigs.Add(config);
            return config;
        }

        protected ConventionConfig Properties<TProperty>()
        {
            var underlyingType = Types.Of<TProperty>();
            var config = new ConventionConfig().Filter(p => p.PropertyType == underlyingType);
            _conventionConfigs.Add(config);
            return config;
        }
    }
}