using Cosmos.Data.Core;
using Cosmos.Dependency;

namespace Cosmos.Dapper.Core.Configs
{
    public static class DapperGlobalRegistrar
    {
        public static void RegisterForCosmosDapper()
        {
            SystemSupportRegistrar.AddDescriptorOnce("CosmosDapper", config =>
            {
                var optAccessorDescriptor = RegisterProxyDescriptor.Create<DapperOptionsAccessor>(RegisterProxyLifetimeType.Singleton);
                config.Configure(bag => bag.Register(optAccessorDescriptor));
            });
        }
    }
}