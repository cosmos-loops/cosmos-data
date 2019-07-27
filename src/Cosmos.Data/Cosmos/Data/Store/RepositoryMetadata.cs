using System;

namespace Cosmos.Data.Store
{
    public sealed class RepositoryMetadata
    {
        public string Name { get; set; }

        public Type ServiceType { get; set; }

        public Type ImplementationType { get; set; }
    }
}