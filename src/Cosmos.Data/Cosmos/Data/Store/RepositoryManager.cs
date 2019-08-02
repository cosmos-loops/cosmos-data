using System;
using System.Collections.Concurrent;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Repository manager
    /// </summary>
    public sealed class RepositoryManager
    {
        private readonly ConcurrentDictionary<Type, RepositoryMetadata> _repositoryMetadata;

        /// <summary>
        /// Create a new instance of <see cref="RepositoryManager"/>
        /// </summary>
        public RepositoryManager()
        {
            _repositoryMetadata = new ConcurrentDictionary<Type, RepositoryMetadata>();
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="implementationType"></param>
        /// <param name="metadata"></param>
        public void Register(Type implementationType, RepositoryMetadata metadata)
        {
            _repositoryMetadata.TryAdd(implementationType, metadata);
        }

        /// <summary>
        /// Require by given type
        /// </summary>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        public RepositoryMetadata Require(Type implementationType)
        {
            return _repositoryMetadata.TryGetValue(implementationType, out var metadata)
                ? metadata
                : default;
        }

        /// <summary>
        /// Require by typed argument
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public RepositoryMetadata Require<TImplementation>() where TImplementation : class, IRepository
        {
            return Require(typeof(TImplementation));
        }
    }
}