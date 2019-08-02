using System;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Repository Map to class attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class RepositoryMapToClassAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="RepositoryMapToClassAttribute"/>
        /// </summary>
        /// <param name="targetType"></param>
        public RepositoryMapToClassAttribute(Type targetType)
        {
            ImplementationType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        /// <summary>
        /// Gets implementation type
        /// </summary>
        public Type ImplementationType { get; }
    }
}