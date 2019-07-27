using System;

namespace Cosmos.Dapper.EntityMapping
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name, bool caseSensitive = true)
        {
            Name = name;
            CaseSensitive = caseSensitive;
        }

        public string Name { get; }

        public bool CaseSensitive { get; }
    }
}
