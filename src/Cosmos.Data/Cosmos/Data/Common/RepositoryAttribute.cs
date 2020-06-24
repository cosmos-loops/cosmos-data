using System;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Attribute of repository
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = false)]
    public class RepositoryAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of repository attribute
        /// </summary>
        /// <param name="name"></param>
        public RepositoryAttribute(string name)
        {
            Name = name;
            MapTo = null;
        }

        /// <summary>
        /// Create a new instance of repository attribute
        /// </summary>
        /// <param name="name"></param>
        /// <param name="targetType"></param>
        public RepositoryAttribute(string name, Type targetType)
        {
            Name = name;
            MapTo = targetType;
        }

        /// <summary>
        /// Repository name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Map to type
        /// </summary>
        public Type MapTo { get; }
    }
}