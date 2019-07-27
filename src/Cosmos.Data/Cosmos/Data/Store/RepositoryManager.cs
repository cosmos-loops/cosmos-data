using System;
using System.Collections.Concurrent;

namespace Cosmos.Data.Store
{
    public sealed class RepositoryManager
    {
        private readonly ConcurrentDictionary<Type, RepositoryMetadata> _repositoryMetadata;

        public RepositoryManager()
        {
            _repositoryMetadata = new ConcurrentDictionary<Type, RepositoryMetadata>();
        }

        public void Register(Type implementationType, RepositoryMetadata metadata)
        {
            _repositoryMetadata.TryAdd(implementationType, metadata);
        }

        public RepositoryMetadata Require(Type implementationType)
        {
            return _repositoryMetadata.TryGetValue(implementationType, out var metadata)
                ? metadata
                : default;
        }

        public RepositoryMetadata Require<TImplementation>() where TImplementation : class, IRepository
        {
            return Require(typeof(TImplementation));
        }
    }
}