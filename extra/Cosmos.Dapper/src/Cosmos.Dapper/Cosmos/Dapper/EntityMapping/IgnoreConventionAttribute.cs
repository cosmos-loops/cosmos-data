using System;

namespace Cosmos.Dapper.EntityMapping
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreConventionAttribute : Attribute { }
}