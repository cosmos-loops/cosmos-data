namespace Cosmos.Data.Common
{
    /// <summary>
    /// Interface of Store Context
    /// </summary>
    public interface IStoreContext : IDbContext
    {
        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();
    }
}