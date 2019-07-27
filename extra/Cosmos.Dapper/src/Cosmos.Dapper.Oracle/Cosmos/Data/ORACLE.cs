using Cosmos.Dapper;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Data.Statements.Dialects;
using Cosmos.Domain.Core;
using Oracle.ManagedDataAccess.Client;
using SqlKata.Compilers;

namespace Cosmos.Data
{
    public static class OracleDapper
    {
        public static IDapperImplementor GetClient(string connectionString, int? timeout = null)
        {
            var options = new DapperOptions
            {
                Name = connectionString.GetHashCode().ToString(),
                ConnectionString = connectionString,
                Timeout = timeout
            };

            ISqlKataCompilerCreator sqlKataCompiler = new SqlKataCompilerCreator<PostgresCompiler>();
            var mappingConfig = new DapperConfig(new OracleDialect(), sqlKataCompiler, options, false);
            return new DapperImplementor(mappingConfig, new SQLGenerator(mappingConfig));
        }

        private static DapperOptions CreateOptions(string connectionString, int? timeout = null)
        {
            return new DapperOptions
            {
                Name = connectionString.GetHashCode().ToString(),
                ConnectionString = connectionString,
                Timeout = timeout
            };
        }

        private static DapperConfig CreateMappingConfig(DapperOptions options)
        {
            ISqlKataCompilerCreator sqlKataCompiler = new SqlKataCompilerCreator<PostgresCompiler>();
            return new DapperConfig(new OracleDialect(), sqlKataCompiler, options, false);
        }

        public static ISQLActionEntry GetDapperAction(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new OracleContextParams(mappingConfig);
            return connector.GetActionEntry(contextParams, null);
        }

        public static ISQLActionEntry<TEntity> GetDapperAction<TEntity>(string connectionString, int? timeout = null)
            where TEntity : class, IEntity, new()
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new OracleContextParams(mappingConfig);
            return connector.GetActionEntry<TEntity>(contextParams, null);
        }

        public static ISQLActionAsyncEntry GetDapperAsynchronousAction(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new OracleContextParams(mappingConfig);
            return connector.GetAsynchronousActionEntry(contextParams, null);
        }

        public static ISQLActionAsyncEntry<TEntity> GetDapperAsynchronousAction<TEntity>(string connectionString, int? timeout = null)
            where TEntity : class, IEntity, new()
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new OracleContextParams(mappingConfig);
            return connector.GetAsynchronousActionEntry<TEntity>(contextParams, null);
        }
        
        public static QueryBuilder GetSqlKataQueryBuilder(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            return new QueryBuilder(connector, mappingConfig.GetCompiler(), options);
        }

        public static EntityQueryBuilder GetSqlKataEntityQueryBuilder(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            return new EntityQueryBuilder(connector, mappingConfig.GetCompiler(), options);
        }

        public static MultipleQueryBuilder GetSqlKataMultipleQueryBuilder(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<OracleConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            return new MultipleQueryBuilder(connector, mappingConfig.GetCompiler());
        }
    }
}