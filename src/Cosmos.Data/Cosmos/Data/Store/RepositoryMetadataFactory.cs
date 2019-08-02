using System;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Repository metadata factory
    /// </summary>
    public static class RepositoryMetadataFactory
    {
        /// <summary>
        /// Create a new <see cref="RepositoryMetadata"/>
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public static RepositoryMetadata Create<TService, TImplementation>(string name)
            where TService : class
            where TImplementation : class, TService
        {
            return Create(typeof(TService), typeof(TImplementation), name);
        }

        /// <summary>
        /// Create a new <see cref="RepositoryMetadata"/>
        /// </summary>
        /// <param name="typeofService"></param>
        /// <param name="typeofImplementaion"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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