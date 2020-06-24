using System;
using System.Collections.Concurrent;
using System.Data;
using Cosmos.Data.Core.UnitOfWork;

namespace Cosmos.Data.Common.UnitOfWork
{
    /// <summary>
    /// UnitOfWork Manager.
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        /// <summary>
        /// Gets or sets the strategy name for holding.
        /// </summary>
        protected virtual string UsingStrategyName { get; set; } = UowRefStrategyFunctions.DefaultName;

        /// <summary>
        /// Current strategy
        /// </summary>
        private IUowRefStrategy CurrentStrategy { get; set; }

        #region Ctor and init

        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkManager"/>.
        /// </summary>
        public UnitOfWorkManager() : this(null, null, true) { }

        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkManager"/>.
        /// </summary>
        public UnitOfWorkManager(string name, Func<IUowRefStrategy> overrideStrategy = null)
            : this(name, overrideStrategy, true) { }

        /// <summary>
        /// Create a new instance of <see cref="UnitOfWorkManager"/>.
        /// </summary>
        public UnitOfWorkManager(string name, Func<IUowRefStrategy> overrideStrategy, bool scopedMode)
            => Init(name, overrideStrategy, scopedMode);

        private void Init(string name, Func<IUowRefStrategy> overrideStrategy, bool scopedMode)
        {
            var runtimeName = string.IsNullOrWhiteSpace(name)
                ? UsingStrategyName
                : name;

            CurrentStrategy = overrideStrategy is null
                ? UowRefStrategyFunctions.For(runtimeName).Invoke()
                : UowRefStrategyFunctions.For(runtimeName, overrideStrategy).Invoke();

            if (scopedMode)
            {
                var state = UnitOfWorkManagerHolder.CurrentIsVirtual;

                if (!state.HasValue)
                {
                    UnitOfWorkManagerHolder.Instance = this;
                }
                else if (state.Value)
                {
                    var instance = UnitOfWorkManagerHolder.Instance;

                    if (instance is UnitOfWorkManagerHolder holder)
                    {
                        var mgr = holder.HoldingManager;
                        CurrentStrategy.Merge(mgr.CurrentStrategy);
                        AdditionalDataDict = new ConcurrentDictionary<string, object>(mgr.AdditionalDataDict);
                    }
                    else if (instance is UnitOfWorkManager mgr)
                    {
                        CurrentStrategy.Merge(mgr.CurrentStrategy);
                        AdditionalDataDict = new ConcurrentDictionary<string, object>(mgr.AdditionalDataDict);
                    }
                    else
                    {
                        AdditionalDataDict = new ConcurrentDictionary<string, object>();
                    }

                    UnitOfWorkManagerHolder.Instance = this;
                }
            }
            else
            {
                AdditionalDataDict = new ConcurrentDictionary<string, object>();
            }
        }

        #endregion

        #region Current UoW-Entry

        /// <summary>
        /// Gets current unit of work entry
        /// </summary>
        public IUnitOfWorkEntry Current => CurrentStrategy.Current;

        #endregion

        #region Repo Binding

        /// <summary>
        /// Binding repository to this unit of work manager
        /// </summary>
        /// <param name="repository"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Binding(IRepository repository) => CurrentStrategy.Binding(repository);

        #endregion

        #region Create UnitOfWork

        /// <inheritdoc />
        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => CurrentStrategy.CreateUnitOfWork(transWrapper, types, level);

        /// <inheritdoc />
        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => CurrentStrategy.CreateUnitOfWork(transWrapper, manualCommit, types, level);

        /// <inheritdoc />
        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposedAction,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => CurrentStrategy.CreateUnitOfWork(transWrapper, disposedAction, types, level);

        /// <inheritdoc />
        public IUnitOfWorkEntry CreateUnitOfWork(ITransactionWrapper transWrapper, Action disposedAction, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null)
            => CurrentStrategy.CreateUnitOfWork(transWrapper, disposedAction, manualCommit, types, level);

        #endregion

        /// <summary>
        /// To flag this manager is virtual manager or not.
        /// </summary>
        public bool IsVirtualManager => false;

        #region Additional Data

        private ConcurrentDictionary<string, object> AdditionalDataDict { get; set; }

        /// <summary>
        /// Get additional data
        /// </summary>
        /// <param name="keyOfData"></param>
        /// <returns></returns>
        public object GetAdditionalData(string keyOfData)
        {
            return AdditionalDataDict.TryGetValue(keyOfData!, out var ret)
                ? ret
                : null;
        }

        /// <summary>
        /// Get additional data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public TData GetAdditionalData<TData>()
        {
            return AdditionalDataDict.TryGetValue(typeof(TData).FullName!, out var mid)
                ? mid is TData ret
                    ? ret
                    : default
                : default;
        }

        /// <summary>
        /// Get additional data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public TData GetAdditionalData<TData>(string keyOfTypedData)
        {
            return AdditionalDataDict.TryGetValue($"{typeof(TData).FullName!}_{keyOfTypedData}", out var mid)
                ? mid is TData ret
                    ? ret
                    : default
                : default;
        }

        /// <summary>
        /// Set additional data
        /// </summary>
        /// <param name="keyOfData"></param>
        /// <param name="data"></param>
        public void SetAdditionalData(string keyOfData, object data)
        {
            AdditionalDataDict.TryAdd(keyOfData, data);
        }

        /// <summary>
        /// Set additional data
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="TData"></typeparam>
        public void SetAdditionalData<TData>(TData data)
        {
            AdditionalDataDict.TryAdd(typeof(TData).FullName!, data);
        }

        /// <summary>
        /// Set additional data
        /// </summary>
        /// <param name="keyOfTypedData"></param>
        /// <param name="data"></param>
        /// <typeparam name="TData"></typeparam>
        public void SetAdditionalData<TData>(string keyOfTypedData, TData data)
        {
            AdditionalDataDict.TryAdd($"{typeof(TData).FullName!}_{keyOfTypedData}", data);
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose
        /// </summary>
        ~UnitOfWorkManager() => Dispose();

        /// <inheritdoc />
        public void Dispose()
        {
            CurrentStrategy.OnDispose = () =>
            {
                AdditionalDataDict.Clear();
                AdditionalDataDict = null;
            };

            CurrentStrategy?.Dispose();
            
            CurrentStrategy = null;
        }

        #endregion
    }
}