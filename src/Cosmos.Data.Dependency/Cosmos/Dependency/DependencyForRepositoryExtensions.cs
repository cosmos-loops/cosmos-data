using Cosmos.Data.Common;

namespace Cosmos.Dependency
{
    /// <summary>
    /// To register repository for Cosmos.Data.
    /// </summary>
    public static class DependencyForRepositoryExtensions
    {
        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="register"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void RegisterRepository<TService, TImplement>(this DependencyProxyRegister register)
            where TService : IRepository
            where TImplement : class, TService
        {
            var reflector = RepositoryReflector.Create<TService, TImplement>();

            RepositoryManager.Instance.Register(reflector);

            register.AddScoped<TService, TImplement>();
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="register"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static void RegisterRepository<TService>(this DependencyProxyRegister register)
            where TService : class, IRepository
        {
            var reflector = RepositoryReflector.Create<TService>();

            RepositoryManager.Instance.Register(reflector);

            register.AddScoped<TService>();
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="register"></param>
        /// <typeparam name="TRegister"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        public static TRegister RegisterRepository<TRegister, TService, TImplement>(this TRegister register)
            where TRegister : DependencyProxyRegister
            where TService : IRepository
            where TImplement : class, TService
        {
            var reflector = RepositoryReflector.Create<TService, TImplement>();

            RepositoryManager.Instance.Register(reflector);

            register.AddScoped<TService, TImplement>();

            return register;
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="register"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TRegister"></typeparam>
        /// <returns></returns>
        public static TRegister RegisterRepository<TRegister, TService>(this TRegister register)
            where TRegister : DependencyProxyRegister
            where TService : class, IRepository
        {
            var reflector = RepositoryReflector.Create<TService>();

            RepositoryManager.Instance.Register(reflector);

            register.AddScoped<TService>();

            return register;
        }
    }
}