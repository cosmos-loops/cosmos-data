using System;

namespace Cosmos.Dapper.EntityMapping
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string name, bool caseSensitive = true)
        {
            Name = name;
            CaseSensitive = caseSensitive;
        }

        public string Name { get; }

        public bool CaseSensitive { get; }
    }
}