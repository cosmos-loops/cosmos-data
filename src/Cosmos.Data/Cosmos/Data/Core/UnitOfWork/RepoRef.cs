using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Repo-Ref
    /// </summary>
    public class RepoRef
    {
        /// <summary>
        /// Repo-Ref
        /// </summary>
        /// <param name="repository"></param>
        public RepoRef(IRepository repository)
        {
            Entry = repository;
            OriginalUnitOfWork = repository.UnitOfWork;
        }

        /// <summary>
        /// Entry
        /// </summary>
        public IRepository Entry { get; set; }

        /// <summary>
        /// Original uow
        /// </summary>
        public IUnitOfWorkEntry OriginalUnitOfWork { get; set; }
    }
}