using System;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Interface of repository metadata
    /// </summary>
    public interface IRepositoryMetadata
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Service type
        /// </summary>
        Type ServiceType { get; set; }

        /// <summary>
        /// Implementation type
        /// </summary>
        Type ImplementType { get; set; }
    }
}