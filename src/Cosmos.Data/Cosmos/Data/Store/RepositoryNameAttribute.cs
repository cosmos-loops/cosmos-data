using System;

namespace Cosmos.Data.Store
{
    /// <summary>
    /// Repository name attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RepositoryNameAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="RepositoryNameAttribute"/>
        /// </summary>
        public RepositoryNameAttribute() { }

        /// <summary>
        /// Create a new instance of <see cref="RepositoryNameAttribute"/>
        /// </summary>
        /// <param name="name"></param>
        public RepositoryNameAttribute(string name) => Name = name;

        /// <summary>
        /// Gets name
        /// </summary>
        public string Name { get; }
    }
}