using System;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Repository metadata
    /// </summary>
    public sealed class RepositoryMetadata
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Service type
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Implementation type
        /// </summary>
        public Type ImplementationType { get; set; }
    }
}