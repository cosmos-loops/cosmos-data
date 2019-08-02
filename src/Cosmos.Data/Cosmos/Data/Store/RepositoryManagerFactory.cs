namespace Cosmos.Data.Store
{
    /// <summary>
    /// Repository Manager Factory
    /// </summary>
    public static class RepositoryManagerFactory
    {
        // ReSharper disable once InconsistentNaming
        private static readonly RepositoryManager _manager = new RepositoryManager();

        /// <summary>
        /// Gets or creates a new instance of <see cref="RepositoryManager"/>
        /// </summary>
        /// <returns></returns>
        public static RepositoryManager CreateInstance() => _manager;
    }
}