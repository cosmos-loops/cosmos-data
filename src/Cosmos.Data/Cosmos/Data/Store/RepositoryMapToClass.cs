using System;

namespace Cosmos.Data.Store
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class RepositoryMapToClassAttribute : Attribute
    {
        public RepositoryMapToClassAttribute(Type targetType)
        {
            ImplementationType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public Type ImplementationType { get; }
    }
}