using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Repo-Ref
    /// </summary>
    public class RepoLink
    {
        /// <summary>
        /// Repo-Ref
        /// </summary>
        /// <param name="repository"></param>
        public RepoLink(IRepository repository)
        {
            Target = repository;
            OriginalUnitOfWork = repository.UnitOfWork;
        }

        /// <summary>
        /// Target
        /// </summary>
        public IRepository Target { get; set; }

        /// <summary>
        /// Original uow
        /// </summary>
        public IUnitOfWorkEntry OriginalUnitOfWork { get; set; }
    }
}