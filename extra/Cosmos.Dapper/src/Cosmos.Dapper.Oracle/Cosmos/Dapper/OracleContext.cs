using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using Oracle.ManagedDataAccess.Client;

namespace Cosmos.Dapper
{
    public abstract class OracleContext<TContext> : DapperContext<TContext, OracleConnection>
        where TContext : DapperContext<TContext, OracleConnection>, IDapperContext
    {
        protected OracleContext(DapperOptions options) : base(options.ToConn(),
            new OracleContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        protected override Type EntityMapType => typeof(IOracleEntityMap);
    }
}