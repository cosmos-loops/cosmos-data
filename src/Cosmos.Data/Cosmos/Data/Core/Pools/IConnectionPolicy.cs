using Cosmos.Disposables.ObjectPools;

namespace Cosmos.Data.Core.Pools
{
    /// <summary>
    /// Interface of connection policy
    /// </summary>
    /// <typeparam name="TConn"></typeparam>
    public interface IConnectionPolicy<TConn> : IPolicy<TConn> { }
}