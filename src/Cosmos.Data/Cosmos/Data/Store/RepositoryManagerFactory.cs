namespace Cosmos.Data.Store
{
    public static class RepositoryManagerFactory
    {
        private static readonly RepositoryManager _manager = new RepositoryManager();

        public static RepositoryManager CreateInstance() => _manager;
    }
}
