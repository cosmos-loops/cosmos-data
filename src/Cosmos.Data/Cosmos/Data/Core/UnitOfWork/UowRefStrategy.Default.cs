using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Cosmos.Data.Common;
using Cosmos.Data.Common.UnitOfWork;

// ReSharper disable InconsistentNaming

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// UoW-Ref strategy function
    /// </summary>
    public class DefaultUowRefStrategy : IUowRefStrategy
    {
        /// <summary>
        /// Protected property - All Uow-Ref list
        /// </summary>
        protected readonly List<UowRef> _allUowRefList = new List<UowRef>();
        
        /// <summary>
        /// Protected property - Registered Uow-Ref list
        /// </summary>
        protected readonly List<UowRef> _registeredUowRefs = new List<UowRef>();
        
        /// <summary>
        /// Protected property - Bundled Repo-Ref list
        /// </summary>
        protected readonly List<RepoRef> _bundledRepoRefs = new List<RepoRef>();

        /// <inheritdoc />
        public virtual string Name { get; } = UowRefStrategyFunctions.DefaultName;

        #region Xxx-Ref Export

        /// <inheritdoc />
        public IEnumerable<UowRef> AllUowRefs => _allUowRefList.AsReadOnly();

        /// <inheritdoc />
        public IEnumerable<UowRef> RegisteredUowRefs => _registeredUowRefs.AsReadOnly();

        /// <inheritdoc />
        public IEnumerable<RepoRef> BundledRepoRefs => _bundledRepoRefs.AsReadOnly();

        #endregion

        #region Xxx-Ref Merge

        /// <inheritdoc />
        public void Merge(IUowRefStrategy strategy)
        {
            _allUowRefList.AddRange(strategy.AllUowRefs);
            _registeredUowRefs.AddRange(strategy.RegisteredUowRefs);
            _bundledRepoRefs.AddRange(strategy.BundledRepoRefs);
        }

        #endregion

        #region Current

        /// <inheritdoc />
        public IUnitOfWorkEntry Current => _allUowRefList.LastOrDefault()?.Entry;

        #endregion

        #region Repo Opts

        /// <inheritdoc />
        public virtual void Binding(IRepository repository)
        {
            var repoRef = new RepoRef(repository);
            repository.UnitOfWork = Current;
            _bundledRepoRefs.Add(repoRef);
        }

        private void SetUowIntoAllRepository()
        {
            _bundledRepoRefs.ForEach(repoRef => repoRef.Entry.UnitOfWork = Current ?? repoRef.OriginalUnitOfWork);
        }

        #endregion

        #region UoW Opts

        /// <inheritdoc />
        public virtual IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
        {
            return CreateAndReturnUowEntry(transWrapper, null, false, types, level);
        }

        /// <inheritdoc />
        public virtual IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
        {
            return CreateAndReturnUowEntry(transWrapper, null, manualCommit, types, level);
        }

        /// <inheritdoc />
        public virtual IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposedAction,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
        {
            return CreateAndReturnUowEntry(transWrapper, disposedAction, false, types, level);
        }

        /// <inheritdoc />
        public virtual IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposedAction, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
        {
            return CreateAndReturnUowEntry(transWrapper, disposedAction, manualCommit, types, level);
        }

        /// <summary>
        /// Create and return UoW-Entry
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="disposedAction"></param>
        /// <param name="manualCommit"></param>
        /// <param name="types"></param>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual IUnitOfWorkEntry CreateAndReturnUowEntry(ITransactionWrapper transWrapper,
            Action disposedAction, bool manualCommit, UnitOfWorkTypes types, IsolationLevel? isolationLevel)
        {
            switch (types)
            {
                case UnitOfWorkTypes.Required:
                    return CreateAndReturnVirtualEntry() ?? CreateAndReturnEntity(transWrapper, disposedAction, manualCommit, isolationLevel);
                case UnitOfWorkTypes.Supports:
                    return CreateAndReturnVirtualEntry() ?? CreateAndReturnNothingEntry(_allUowRefList.LastOrDefault()?.IsNotSupported ?? false);
                case UnitOfWorkTypes.Mandatory:
                    return CreateAndReturnVirtualEntry() ?? throw CreateAndReturnExceptionForMandatory();
                case UnitOfWorkTypes.NotSupported:
                    return CreateAndReturnNothingEntry(true);
                case UnitOfWorkTypes.Never:
                    return CreateAndReturnNever();
                case UnitOfWorkTypes.Nested:
                    return CreateAndReturnEntity(transWrapper, disposedAction, manualCommit, isolationLevel);
                default:
                    throw new NotImplementedException();
            }
        }

        private IUnitOfWorkEntry CreateAndReturnVirtualEntry()
        {
            var isNotSupported = _allUowRefList.LastOrDefault()?.IsNotSupported ?? false;

            if (!isNotSupported)
            {
                for (var i = _registeredUowRefs.Count - 1; i >= 0; i--)
                {
                    if (_registeredUowRefs[i].Entry.GetOrBegin(false) != null)
                    {
                        var uowRef = UowRefNew.Virtual(_registeredUowRefs[i].Entry, false, @ref => _allUowRefList.Remove(@ref));
                        _allUowRefList.Add(uowRef);
                        SetUowIntoAllRepository();

                        return uowRef.Entry;
                    }
                }
            }

            return null;
        }

        private IUnitOfWorkEntry CreateAndReturnNothingEntry(bool isNotSupported)
        {
            var uowRef = UowRefNew.Nothing(isNotSupported, @ref => _allUowRefList.Remove(@ref));

            _allUowRefList.Add(uowRef);
            SetUowIntoAllRepository();

            return uowRef.Entry;
        }

        private IUnitOfWorkEntry CreateAndReturnEntity(ITransactionWrapper transWrapper, Action disposedAction, bool manualCommit, IsolationLevel? isolationLevel)
        {
            var baseUowEntry = disposedAction is null
                ? new UnitOfWorkEntry(transWrapper, manualCommit)
                : new UnitOfWorkEntry(transWrapper, disposedAction, manualCommit);

            var uowRef = UowRefNew.Original(baseUowEntry, isolationLevel, @ref =>
            {
                _registeredUowRefs.Remove(@ref);
                _allUowRefList.Remove(@ref);
                SetUowIntoAllRepository();
            });

            _registeredUowRefs.Add(uowRef);
            _allUowRefList.Add(uowRef);
            SetUowIntoAllRepository();

            return uowRef.Entry;
        }

        private IUnitOfWorkEntry CreateAndReturnNever()
        {
            var isNotSupported = _allUowRefList.LastOrDefault()?.IsNotSupported ?? false;

            if (!isNotSupported)
            {
                for (var i = _registeredUowRefs.Count - 1; i >= 0; i--)
                {
                    if (_registeredUowRefs[i].Entry.GetOrBegin(false) != null)
                    {
                        throw CreateAndReturnExceptionForNever();
                    }
                }
            }

            return CreateAndReturnNothingEntry(isNotSupported);
        }

        private static Exception CreateAndReturnExceptionForMandatory()
        {
            return new InvalidOperationException("Propagation type is: Mandatory. Use the current transaction, if there is no current transaction, an exception is thrown.");
        }

        private static Exception CreateAndReturnExceptionForNever()
        {
            return new InvalidOperationException("Propagation type is: Never.");
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose
        /// </summary>
        ~DefaultUowRefStrategy() => Dispose();

        /// <summary>
        /// Internal dispose counter
        /// </summary>
        int _disposeCounter;

        /// <summary>
        /// On dispose
        /// </summary>
        public Action OnDispose { get; set; }

        /// <inheritdoc />
        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposeCounter) != 1)
                return;

            try
            {
                Exception exception = null;

                for (var i = _registeredUowRefs.Count - 1; i >= 0; i++)
                {
                    try
                    {
                        if (exception is null)
                            _registeredUowRefs[i].Entry.Commit();
                        else
                            _registeredUowRefs[i].Entry.Rollback();
                    }
                    catch (Exception ex)
                    {
                        exception ??= ex;
                    }
                }

                if (exception != null)
                    throw exception;
            }
            finally
            {
                OnDispose?.Invoke();

                _registeredUowRefs.Clear();
                _allUowRefList.Clear();
                _bundledRepoRefs.Clear();

                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}