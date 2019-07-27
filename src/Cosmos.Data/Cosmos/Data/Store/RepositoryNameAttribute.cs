using System;

namespace Cosmos.Data.Store
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RepositoryNameAttribute : Attribute
    {
        public RepositoryNameAttribute() { }
        public RepositoryNameAttribute(string name) => Name = name;

        public string Name { get; }
    }
}