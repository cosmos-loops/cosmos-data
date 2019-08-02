using System;
using Cosmos.Dependency;

namespace Cosmos.Data.Context
{
    /// <summary>
    /// Interface of DbContext config
    /// </summary>
    public interface IDbContextConfig
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        IDbContextConfig Configure(Action<RegisterProxyBag> configureAction);
    }
}