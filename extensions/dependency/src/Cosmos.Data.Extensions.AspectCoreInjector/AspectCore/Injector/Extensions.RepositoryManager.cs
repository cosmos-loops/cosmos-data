using System;
using System.Linq;
using System.Reflection;
using Cosmos.Data.Store;

namespace AspectCore.Injector
{
    /// <summary>
    /// To register repository of Cosmos.Data
    /// </summary>
    public static class RepositoryManagerExtensions
    {
        public static IServiceContainer AddRepository<TService, TImplementation>(this IServiceContainer services, string repositoryName = "")
            where TService : class, IRepository
            where TImplementation : class, TService
        {
            services.AddType<TService, TImplementation>(Lifetime.Scoped);

            var manager = RepositoryManagerFactory.CreateInstance();

            var typeofService = typeof(TService);
            var typeofImplement = typeof(TImplementation);

            var name = repositoryName;
            if (string.IsNullOrWhiteSpace(name))
                name = GetRepositoryName(typeofService);
            if (string.IsNullOrWhiteSpace(name))
                name = GetRepositoryName(typeofImplement);
            if (string.IsNullOrWhiteSpace(name))
                name = GetTypeName(typeofImplement);

            var meta = RepositoryMetadataFactory.Create(typeofService, typeofImplement, name);

            manager.Register(typeofImplement, meta);

            return services;
        }

        private static string GetRepositoryName(Type type)
        {
            var attr = type.GetCustomAttributes<RepositoryNameAttribute>().FirstOrDefault();
            return attr?.Name ?? string.Empty;
        }

        private static string GetTypeName(Type type)
        {
            return type.FullName;
        }
    }
}
