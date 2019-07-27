using System;
using System.Data.SqlClient;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;

namespace Cosmos.Dapper
{
    public abstract class SqlServerContext<TContext> : DapperContext<TContext, SqlConnection>
        where TContext : DapperContext<TContext, SqlConnection>, IDapperContext
    {
        protected SqlServerContext(DapperOptions options) : base(options.ToConn(),
            new SqlServerContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        protected override Type EntityMapType => typeof(ISqlServerEntityMap);
    }
}