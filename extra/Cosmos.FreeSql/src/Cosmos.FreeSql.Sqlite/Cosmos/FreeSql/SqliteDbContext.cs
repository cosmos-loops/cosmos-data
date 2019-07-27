using System;
using Cosmos.Data.Context;
using Cosmos.Data.Transaction;
using Cosmos.FreeSql.Map;
using FreeSql;

namespace Cosmos.FreeSql
{
    public class SqliteDbContext<TDbContext> : DbContextBase, ISqliteDbContext
        where TDbContext : DbContextBase, ISqliteDbContext, IDbContext
    {
        private readonly IFreeSql<TDbContext> _freeSql;

        // ReSharper disable once StaticMemberInGenericType
        // ReSharper disable once InconsistentNaming
        private static readonly Type _typeMapType = typeof(ISqliteEntityMap);

        public SqliteDbContext(IFreeSql<TDbContext> freeSql, ITransactionCallingWrapper transactionCallingWrapper)
            : base(transactionCallingWrapper)
        {
            _freeSql = freeSql ?? throw new ArgumentNullException(nameof(freeSql));
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseFreeSql(_freeSql);

            using (var scanner = new EntityMapScanner<TDbContext>(_typeMapType))
            {
                foreach (var map in scanner.ScanAndReturnInstances())
                {
                    map?.Map(_freeSql.CodeFirst);
                }
            }
        }
    }
}