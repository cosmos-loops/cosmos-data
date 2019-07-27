using Dapper;

namespace Cosmos.Dapper.Core.Binders
{
    public static class SyncBindingManager
    {
        public static void Sync(IClassMapGetter mappingConfig)
        {
            SqlMapper.TypeMapProvider = t => new BindingTypeMap(t, mappingConfig.GetMap(t));
        }
    }
}