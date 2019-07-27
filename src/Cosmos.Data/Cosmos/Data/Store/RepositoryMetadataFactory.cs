using System;

namespace Cosmos.Data.Store
{
    public static class RepositoryMetadataFactory
    {
        public static RepositoryMetadata Create<TService, TImplementation>(string name)
            where TService : class
            where TImplementation : class, TService
        {
            return Create(typeof(TService), typeof(TImplementation), name);
        }

        public static RepositoryMetadata Create(Type typeofService, Type typeofImplementaion, string name)
        {
            return new RepositoryMetadata
            {
                ServiceType = typeofService,
                ImplementationType = typeofImplementaion,
                Name = name
            };
        }
    }
}
