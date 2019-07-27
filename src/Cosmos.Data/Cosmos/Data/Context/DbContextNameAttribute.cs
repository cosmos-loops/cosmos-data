using System;

namespace Cosmos.Data.Context
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DbContextNameAttribute : Attribute
    {
        public DbContextNameAttribute(string name) => Name = name;

        public string Name { get; }
    }
}