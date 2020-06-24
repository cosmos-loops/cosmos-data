using System;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Interface of repository
    /// </summary>
    [RepositoryLifecycle]
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Gets current trance id
        /// </summary>
        string CurrentTraceId { get; set; }

        /// <summary>
        /// Gets or sets unit of work entry
        /// </summary>
        IUnitOfWorkEntry UnitOfWork { get; set; }
    }

    /// <summary>
    /// Interface of repository
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    [RepositoryRawContext]
    public interface IRepository<TContext> : IRepository where TContext : class, IStoreContext
    {
        /// <summary>
        /// Gets the raw typed DbContext
        /// </summary>
        TContext RawTypedContext { get; set; }
    }
}