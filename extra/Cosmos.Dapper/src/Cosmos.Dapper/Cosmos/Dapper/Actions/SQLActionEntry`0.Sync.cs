using System;
using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public class SQLActionSyncEntry : ISQLActionEntry, IHasRootActionBank, IHasDataFilter, IHasBulkOpt
    {
        SQLActionSetBase IHasRootActionBank.RootActionBank { get; set; }

        IDapperContextParams IHasBulkOpt.ContextParams { get; set; }

        ISQLPredicate[] IHasDataFilter.Filters { get; set; }

        protected IHasRootActionBank ActionBankGetter => this;

        public SQLActionSyncEntry(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, ISQLPredicate[] filters = null)
        {
            var _ = (IHasRootActionBank) this;
            _.RootActionBank = rootActionSet ?? throw new ArgumentNullException(nameof(rootActionSet));

            var __ = (IHasDataFilter) this;
            __.Filters = filters;

            var ___ = (IHasBulkOpt) this;
            ___.ContextParams = contextParams ?? throw new ArgumentNullException(nameof(contextParams));
        }

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

        #region single actions

        public InsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new InsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity);
            return StoreActionToBank(action) as InsertAction<TEntity>;
        }

        public UpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new UpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as UpdateAction<TEntity>;
        }

        public DeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new DeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as DeleteAction<TEntity>;
        }

        #endregion

        #region batch actions

        public BatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BatchInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BatchInsertAction<TEntity>;
        }

        public BatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as BatchUpdateAction<TEntity>;
        }

        public BatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as BatchDeleteAction<TEntity>;
        }

        #endregion

        #region bulk action

        public BulkInsertAction<TEntity> BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BulkInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BulkInsertAction<TEntity>;
        }

        #endregion

        #region expression action

        public ExpressionDeleteAction<TEntity> Delete<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new ExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as ExpressionDeleteAction<TEntity>;
        }

        #endregion

    }
}