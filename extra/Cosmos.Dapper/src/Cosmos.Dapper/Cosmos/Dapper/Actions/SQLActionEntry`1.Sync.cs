using System;
using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Domain.Core;

namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public class SQLActionSyncEntry<TEntity> : SQLActionSyncEntry, ISQLActionEntry<TEntity> where TEntity : class, IEntity, new()
    {
        public SQLActionSyncEntry(SQLActionSet<TEntity> rootActionSet, IDapperContextParams contextParams, ISQLPredicate[] filters = null)
            : base(rootActionSet, contextParams, filters) { }

        #region single actions

        public InsertAction<TEntity> Insert(TEntity entity)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new InsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity);
            return StoreActionToBank(action) as InsertAction<TEntity>;
        }

        public UpdateAction<TEntity> Update(TEntity entity, bool ignoreAllKeyProperties = false)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new UpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as UpdateAction<TEntity>;
        }

        public DeleteAction<TEntity> Delete(TEntity entity)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new DeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as DeleteAction<TEntity>;
        }

        #endregion

        #region batch actions

        public BatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BatchInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BatchInsertAction<TEntity>;
        }

        public BatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as BatchUpdateAction<TEntity>;
        }

        public BatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as BatchDeleteAction<TEntity>;
        }

        #endregion

        #region bulk action

        public BulkInsertAction<TEntity> BulkInsert(IEnumerable<TEntity> entities)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BulkInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BulkInsertAction<TEntity>;
        }

        #endregion

        #region expression action

        public ExpressionDeleteAction<TEntity> Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new ExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as ExpressionDeleteAction<TEntity>;
        }

        #endregion

    }
}