using Cosmos.IdUtils;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Extensions for repository metadata
    /// </summary>
    public static class RepositoryMetadataExtensions
    {
        /// <summary>
        /// Convert to runtime scoped repository metadata
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="instance"></param>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static RuntimeScopedRepositoryMetadata RuntimeScoped(this IRepositoryMetadata metadata, IRepository instance, TraceIdAccessor accessor = null)
        {
            metadata.CheckNull(nameof(metadata));
            return new RuntimeScopedRepositoryMetadata(metadata, instance, accessor);
        }
    }
}