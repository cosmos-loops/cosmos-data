using System;
using System.Data;
using Cosmos.Validation.Annotations;

namespace Cosmos.Data.Common
{
    /// <summary>
    /// Interface of Unit of Work manager
    /// </summary>
    public interface IUnitOfWorkManager : IDisposable
    {
        /// <summary>
        /// Gets current unit of work<br />
        /// 获取当前工作单元
        /// </summary>
        IUnitOfWorkEntry Current { get; }

        /// <summary>
        /// Bind the repository into this manager<br />
        /// 将指定仓储的事务交由本工作单元管理器管理
        /// </summary>
        /// <param name="repository"></param>
        void Binding([NotNull] IRepository repository);

        /// <summary>
        /// Begin an unit of work
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork([NotNull] ITransactionWrapper transWrapper,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// Begin an unit of work with disposable action
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="disposableAction"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork([NotNull] ITransactionWrapper transWrapper, Action disposableAction,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// Begin an unit of work with commit manually
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="manualCommit"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork([NotNull] ITransactionWrapper transWrapper, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// Begin an unit of work with disposable action and commit manually
        /// </summary>
        /// <param name="transWrapper"></param>
        /// <param name="disposableAction"></param>
        /// <param name="manualCommit"></param>
        /// <param name="types"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IUnitOfWorkEntry CreateUnitOfWork([NotNull] ITransactionWrapper transWrapper, Action disposableAction, bool manualCommit,
            UnitOfWorkTypes types = UnitOfWorkTypes.Required, IsolationLevel? level = null);

        /// <summary>
        /// To flag this manager is virtual manager or not.
        /// </summary>
        bool IsVirtualManager { get; }

        /// <summary>
        /// Get additional data
        /// </summary>
        /// <param name="keyOfData"></param>
        /// <returns></returns>
        object GetAdditionalData(string keyOfData);

        /// <summary>
        /// Get additional data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        TData GetAdditionalData<TData>();

        /// <summary>
        /// Get additional data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        TData GetAdditionalData<TData>(string keyOfTypedData);

        /// <summary>
        /// Set additional data
        /// </summary>
        /// <param name="keyOfData"></param>
        /// <param name="data"></param>
        void SetAdditionalData(string keyOfData, object data);

        /// <summary>
        /// Set additional data
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="TData"></typeparam>
        void SetAdditionalData<TData>(TData data);

        /// <summary>
        /// Set additional data
        /// </summary>
        /// <param name="keyOfData"></param>
        /// <param name="data"></param>
        /// <typeparam name="TData"></typeparam>
        void SetAdditionalData<TData>(string keyOfData, TData data);
    }
}