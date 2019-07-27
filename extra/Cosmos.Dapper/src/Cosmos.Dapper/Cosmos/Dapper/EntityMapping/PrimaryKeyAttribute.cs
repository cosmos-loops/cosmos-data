using System;
using Dapper.Mapper;

namespace Cosmos.Dapper.EntityMapping
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute(KeyType keyType = KeyType.Identity) => KeyType = keyType;

        public KeyType KeyType { get; }
    }
}