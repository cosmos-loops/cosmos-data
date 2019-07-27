using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data;
using Cosmos.Data.Context;
using Cosmos.Data.Transaction;
using Nito.AsyncEx.Synchronous;

namespace Cosmos.FreeSql
{
    public abstract class DbContextBase : global::FreeSql.DbContext, IDbContext
    {
        protected DbContextBase(ITransactionCallingWrapper transactionCallingWrapper)
        {
            TransactionCallingWrapper = transactionCallingWrapper ?? NullTransactionCallingWrapper.Instance;
        }

        #region Database and connection

        protected ITransactionCallingWrapper TransactionCallingWrapper { get; }

        private bool IsTransCallingWrapperWorking()
        {
            return TransactionCallingWrapper != null && TransactionCallingWrapper.Count > 0;
        }

        #endregion

        #region Before save changes

        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual void SaveChangesBefore() { }

        #endregion

        #region Save changes

        public override int SaveChanges()
        {
            SaveChangesBefore();
            if (IsTransCallingWrapperWorking())
                return TransactionCommit(TransactionCallingWrapper);
            return base.SaveChanges();
        }

//        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//        {
//            return SaveChangesAsync();
//        }

        public override async Task<int> SaveChangesAsync()
        {
            SaveChangesBefore();
            if (IsTransCallingWrapperWorking())
                return await TransactionCommitAsync(TransactionCallingWrapper);
            return await base.SaveChangesAsync();
        }

        #endregion

        #region Commit

        public void Commit()
        {
            Commit(null);
        }

        public void Commit(Action callback)
        {
            try
            {
                SaveChanges();
                callback?.Invoke();
            }
            catch (Exception ex)
            {
                throw new ConcurrencyException(ex);
            }
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run(() => Commit(null), cancellationToken);
        }

        public async Task CommitAsync(Action callback, CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync();
                callback?.Invoke();
            }
            catch (Exception ex)
            {
                throw new ConcurrencyException(ex);
            }
        }

        private int TransactionCommit(ITransactionCallingWrapper callingWrapper)
        {
            var result = 0;

            using (var uow = Orm.CreateUnitOfWork())
            {
                var transactionWrapper = new FreeSqlTransactionWrapper(uow);

                try
                {
                    callingWrapper.CommitAsync(transactionWrapper.CurrentTransaction).WaitAndUnwrapException();
                    result = base.SaveChanges();
                    transactionWrapper.Commit();
                }
                catch
                {
                    transactionWrapper.Rollback();
                    throw;
                }
            }

            return result;
        }

        private async Task<int> TransactionCommitAsync(ITransactionCallingWrapper callingWrapper, CancellationToken cancellationToken = default)
        {
            var result = 0;

            using (var uow = Orm.CreateUnitOfWork())
            {
                var transactionWrapper = new FreeSqlTransactionWrapper(uow);

                try
                {
                    callingWrapper.CommitAsync(transactionWrapper.CurrentTransaction).WaitAndUnwrapException();
                    result = await base.SaveChangesAsync();
                    transactionWrapper.Commit();
                }
                catch
                {
                    transactionWrapper.Rollback();
                    throw;
                }
            }

            return result;
        }

        #endregion
        
    }
}