using System;
using System.Data.SQLite;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;

namespace Cosmos.Dapper
{
    public abstract class SQLiteContext<TContext> : DapperContext<TContext, SQLiteConnection>
        where TContext : DapperContext<TContext, SQLiteConnection>, IDapperContext
    {
        protected SQLiteContext(DapperOptions options) : base(options.ToConn(),
            new SQLiteContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        protected override Type EntityMapType => typeof(ISQLiteEntityMap);
    }
}