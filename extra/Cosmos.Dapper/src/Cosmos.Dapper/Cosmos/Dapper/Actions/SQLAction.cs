// ReSharper disable InconsistentNaming

using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Data.Statements;
using Cosmos.Data.Transaction;
using Cosmos.Domain.Core;
using Dapper;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public abstract class SQLAction : ISQLAction, IHasRootActionBank, IHasDataFilter, IHasBulkOpt
    {
        SQLActionSetBase IHasRootActionBank.RootActionBank { get; set; }

        IDapperContextParams IHasBulkOpt.ContextParams { get; set; }

        ISQLPredicate[] IHasDataFilter.Filters { get; set; }

        protected IHasRootActionBank ActionBankGetter => this;

        protected readonly IDapperMappingConfig _mappingConfig;
        protected readonly IDapperConnector _connector;

        protected SQLAction(
            SQLActionSetBase rootActionBank,
            ActionKind kind,
            ActionCallingMode callingMode,
            IDapperContextParams contextParams,
            ISQLPredicate[] filters)
        {
            var _ = (IHasRootActionBank) this;
            _.RootActionBank = rootActionBank ?? throw new ArgumentNullException(nameof(rootActionBank));

            _mappingConfig = _.RootActionBank.InternalMappingConfig;
            _connector = _.RootActionBank.InternalConnector;

            var __ = (IHasDataFilter) this;
            __.Filters = filters;

            var ___ = (IHasBulkOpt) this;
            ___.ContextParams = contextParams ?? throw new ArgumentNullException(nameof(contextParams));

            Kind = kind;
            CallingMode = callingMode;
            Options = _mappingConfig.Options;
        }

        public string Dialect => _mappingConfig.Dialect.DialectName;

        public ActionKind Kind { get; }

        public ActionCallingMode CallingMode { get; }


        public DapperOptions Options { get; }

        public bool IsExecuted { get; set; }

        protected ITransactionWrapper TransactionWrapper => _connector.TransactionWrapper;

        #region Add new action

        protected ISQLAction StoreActionToBank(ISQLAction action)
        {
            if (action != null)
            {
                ActionBankGetter.RootActionBank.AddSQLAction(action);
            }

            return action;
        }

        #endregion

        #region Merge filters with global

        protected ISQLPredicate[] MixedDataFilter<TEntity>(ISQLPredicate[] filters) where TEntity : class, IEntity, new()
        {
            var globalFilter = GlobalDataFilterManager.GetFilter((typeof(TEntity), typeof(TEntity)));
            return DataFilterMixer.Mix(globalFilter, filters);
        }

        #endregion

    }
}