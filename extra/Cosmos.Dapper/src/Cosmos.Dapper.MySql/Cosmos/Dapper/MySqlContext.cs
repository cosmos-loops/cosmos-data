using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using MySql.Data.MySqlClient;

namespace Cosmos.Dapper
{
    public abstract class MySqlContext<TContext> : DapperContext<TContext, MySqlConnection>
        where TContext : DapperContext<TContext, MySqlConnection>, IDapperContext
    {
        protected MySqlContext(DapperOptions options)
            : base(options.ToConn(),
                new MySqlContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        protected override Type EntityMapType => typeof(IMySqlEntityMap);
    }
}