using System;
using System.Collections.Generic;
using System.Data;
using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Interface for UoW-Ref strategy function
    /// </summary>
    public interface IUowRefStrategy : IDisposable
    {
        /// <summary>
        /// Strategy name
        /// </summary>
        string Name { get; }

        #region Xxx-Ref Export

        /// <summary>
        /// Export all Uow-Ref info
        /// </summary>
        IEnumerable<UowRef> AllUowRefs { get; }

        /// <summary>
        /// Export all registered Uow-Ref info
        /// </summary>
        IEnumerable<UowRef> RegisteredUowRefs { get; }

        /// <summary>
        /// Export all bundled Repo-Ref info
        /// </summary>
        IEnumerable<RepoRef> BundledRepoRefs { get; }

        #endregion

        #region Xxx-Ref Merge

        /// <summary>
        /// Merge all Xxx-Ref
        /// </summary>
        /// <param name="strategy"></param>
        void Merge(IUowRefStrategy strategy);

        #endregion

        #region Current

        /// <summary>
        /// Gets current unit of work entry
        /// </summary>
        IUnitOfWorkEntry Current { get; }

        #endregion

        #region Repo Opts

        /// <summary>
        /// Binding repository to this unit of work manager
        /// </summary>
        /// <param name="repository"></param>
        void Binding(IRepository repository);

        #endregion

        #region UoW Opts

        /// <summary>
        /// Begin an unit of work
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// Begin an unit of work with disposable action
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="manualCommit"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// Begin an unit of work with commit manually
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="disposedAction"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposedAction,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// Begin an unit of work with disposable action and commit manually
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="disposedAction"></param>
        /// <param name="manualCommit"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposedAction, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        #endregion

        #region Dispose

        /// <summary>
        /// On dispose
        /// </summary>
        Action OnDispose { get; set; }

        #endregion
    }
}