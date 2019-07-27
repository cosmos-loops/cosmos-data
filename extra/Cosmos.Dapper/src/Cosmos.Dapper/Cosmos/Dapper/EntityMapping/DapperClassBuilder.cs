using System;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.EntityMapping
{
    public class DapperClassBuilder
    {
        private readonly DapperConfig _mappingConfig;

        public DapperClassBuilder(DapperConfig config)
        {
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
        }

        public DapperEntityTypeBuilder<TEntity> Entity<TEntity>() where TEntity : class, IEntity
        {
            return new DapperEntityTypeBuilder<TEntity>(_mappingConfig);
        }
    }
}