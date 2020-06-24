using System;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Static metadata of repository
    /// </summary>
    public class RepositoryMetadata : IRepositoryMetadata
    {
        internal RepositoryMetadata() { }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public Type ServiceType { get; set; }

        /// <inheritdoc />
        public Type ImplementType { get; set; }
    }
}