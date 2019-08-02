using System;
using Cosmos.IdUtils;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Scpoed repository metadata
    /// </summary>
    public sealed class ScopedRepositoryMetadata
    {
        /// <summary>
        /// Create a new instance of <see cref="ScopedRepositoryMetadata"/>
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="accessor"></param>
        public ScopedRepositoryMetadata(RepositoryMetadata metadata, TraceIdAccessor accessor = null)
        {
            metadata.CheckNull(nameof(metadata));

            TranceId = accessor?.GetTraceId();

            Name = metadata.Name;
            ServiceType = metadata.ServiceType;
            ImplementationType = metadata.ImplementationType;
        }

        /// <summary>
        /// Trance id
        /// </summary>
        public string TranceId { get; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Service type
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Implementation type
        /// </summary>
        public Type ImplementationType { get; }
    }
}