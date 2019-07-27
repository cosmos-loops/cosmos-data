using System;
using Cosmos.Data.Context;
using Cosmos.Data.Transaction;
using Cosmos.FreeSql.Map;
using FreeSql;

namespace Cosmos.FreeSql
{
    public class MySqlDbContext<TDbContext> : DbContextBase, IMySqlDbContext
        where TDbContext : DbContextBase, IMySqlDbContext, IDbContext
    {
        private readonly IFreeSql<TDbContext> _freeSql;

        // ReSharper disable once StaticMemberInGenericType
        // ReSharper disable once InconsistentNaming
        private static readonly Type _typeMapType = typeof(IMySqlEntityMap);

        public MySqlDbContext(IFreeSql<TDbContext> freeSql, ITransactionCallingWrapper transactionCallingWrapper)
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