using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Repository raw database context initializing interceptor
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Field)]
    public class RepositoryRawContextAttribute : AbstractInterceptorAttribute
    {
        private const string RawTypedContext = "RawTypedContext";

        /// <summary>
        /// Create a new instance of <see cref="RepositoryRawContextAttribute"/>.
        /// </summary>
        public RepositoryRawContextAttribute()
        {
            BindingPropertyName = RawTypedContext;
        }

        /// <summary>
        /// Create a new instance of <see cref="RepositoryRawContextAttribute"/>.
        /// </summary>
        /// <param name="typeOfContext"></param>
        /// <param name="bindingPropertyName"></param>
        public RepositoryRawContextAttribute(Type typeOfContext, string bindingPropertyName = RawTypedContext)
        {
            RawContextType = typeOfContext ?? throw new ArgumentNullException(nameof(typeOfContext));
            BindingPropertyName = bindingPropertyName;
        }

        private Type RawContextType { get; set; }

        private string BindingPropertyName { get; set; }

        /// <inheritdoc />
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var contextClazz = GetRawContextClazz(context.Implementation);

            //Get context instance from service provider
            var instance = context.ServiceProvider.GetService(contextClazz);

            if (instance is null)
                throw new ArgumentException($"Unable to load database context of '{contextClazz.Name}' from IoC container.");

            context.Implementation.SetPropertyValue(BindingPropertyName, instance);

            return next(context);
        }

        private Type GetRawContextClazz(object implementation)
        {
            if (RawContextType != null)
                return RawContextType;

            if (implementation is null)
                throw new ArgumentNullException(nameof(implementation), "AspectContext.Implementation cannot be null.");

            // ReSharper disable once PossibleNullReferenceException
            var genericClazz = implementation.GetType().GetInterfaces()
               .FirstOrDefault(typeOfInterface => typeOfInterface.FullName.StartsWith("Cosmos.Data.Common.IRepository`1["));

            if (genericClazz is null)
                throw new ArgumentException("Unable to get Cosmos.Data.Common.IRepository`1 interface.");

            return genericClazz.GetGenericArguments()[0];
        }
    }
}