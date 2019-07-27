using Cosmos.Dapper.EntityMapping;

namespace Cosmos.Dapper.Core.Mapping
{
    public interface IMap
    {
        void Map(DapperClassBuilder builder);
    }
}