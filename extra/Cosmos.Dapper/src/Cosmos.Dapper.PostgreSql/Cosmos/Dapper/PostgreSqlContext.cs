using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using Npgsql;

namespace Cosmos.Dapper
{
    public abstract class PostgreSqlContext<TContext> : DapperContext<TContext, NpgsqlConnection>
        where TContext : DapperContext<TContext, NpgsqlConnection>, IDapperContext
    {
        protected PostgreSqlContext(DapperOptions options) : base(options.ToConn(),
            new PostgreSqlContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        protected override Type EntityMapType => typeof(IPostgreSqlEntityMap);
    }
}