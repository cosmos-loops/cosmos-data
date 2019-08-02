using System;

namespace Cosmos.Data.Context
{
    /// <summary>
    /// DbContext name attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DbContextNameAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="DbContextNameAttribute"/>
        /// </summary>
        /// <param name="name"></param>
        public DbContextNameAttribute(string name) => Name = name;

        /// <summary>
        /// Gets the name of DbContext
        /// </summary>
        public string Name { get; }
    }
}