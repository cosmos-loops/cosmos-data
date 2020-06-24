using System;
using Cosmos.IdUtils;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Runtime scoped repository metadata
    /// </summary>
    public class RuntimeScopedRepositoryMetadata
    {
        /// <summary>
        /// Create a new instance of <see cref="RuntimeScopedRepositoryMetadata"/>
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="instance"></param>
        /// <param name="accessor"></param>
        internal RuntimeScopedRepositoryMetadata(IRepositoryMetadata metadata, IRepository instance, TraceIdAccessor accessor = null)
        {
            metadata.CheckNull(nameof(metadata));
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            TranceId = accessor?.GetTraceId();

            Name = metadata.Name;
            ServiceType = metadata.ServiceType;
            ImplementType = metadata.ImplementType;
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
        public Type ImplementType { get; }
        
        /// <summary>
        /// Instance of repository
        /// </summary>
        public IRepository Instance { get; }
    }
}