using System;
using Cosmos.Dependency;

namespace Cosmos.Data.Context
{
    public interface IDbContextConfig
    {
        IDbContextConfig Configure(Action<RegisterProxyBag> configureAction);
    }
}
