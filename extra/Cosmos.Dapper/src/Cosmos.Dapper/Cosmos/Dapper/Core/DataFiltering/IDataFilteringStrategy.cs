using System;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DataFiltering
{
    public interface IDataFilteringStrategy
    {
        ISQLPredicate GetFilteringPredicate();
        (Type, Type) GetSignature();
    }
}