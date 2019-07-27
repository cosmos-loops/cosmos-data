using System;
using Cosmos.IdUtils;

namespace Cosmos.Data.Store
{
    public sealed class ScopedRepositoryMetadata
    {
        public ScopedRepositoryMetadata(RepositoryMetadata metadata, TraceIdAccessor accessor = null)
        {
            metadata.CheckNull(nameof(metadata));

            TranceId = accessor?.GetTraceId();

            Name = metadata.Name;
            ServiceType = metadata.ServiceType;
            ImplementationType = metadata.ImplementationType;
        }

        public string TranceId { get; }

        public string Name { get; }

        public Type ServiceType { get; }

        public Type ImplementationType { get; }
    }
}