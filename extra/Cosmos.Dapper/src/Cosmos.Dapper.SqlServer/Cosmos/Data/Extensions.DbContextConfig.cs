using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Cosmos.Dapper;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Binders;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Dapper.Operations;
using Cosmos.Data.Context;
using Cosmos.Data.Statements.Dialects;
using Cosmos.Data.Store;
using Cosmos.Dependency;
using Dapper.Mapper;
using Microsoft.Extensions.Configuration;
using SqlKata.Compilers;

namespace Cosmos.Data
{
    public static class DbContextConfigExtensions
    {

        #region UseSqlServerDapper

        public static IDbContextConfig UseDapperWithSqlServer(
            this IDbContextConfig config,
            string name,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            var options = new DapperOptions {Name = name, ConnectionString = connectionString};
            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServer(
            this IDbContextConfig config,
            string name,
            IConfiguration configuration,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(name))
                name = "default";

            var options = configuration.GetValue<DapperOptions>($"Cosmos:Data:{name}");

            if (options.ConnectionString.IsNullOrWhiteSpace())
                options.ConnectionString = configuration.GetConnectionString(name);

            if (options.Name.IsNullOrWhiteSpace())
                options.Name = name == "default"
                    ? options.ConnectionString.GetHashCode().ToString()
                    : name;

            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServer(
            this IDbContextConfig config,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            var options = new DapperOptions {Name = connectionString.GetHashCode().ToString(), ConnectionString = connectionString};
            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServer(
            this IDbContextConfig config,
            Action<DapperOptions> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            var options = new DapperOptions();
            optionAct?.Invoke(options);
            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServer(
            this IDbContextConfig config,
            IConfiguration configuration,
            Action<DapperOptions> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = new DapperOptions();
            optionAct?.Invoke(options);

            if (string.IsNullOrWhiteSpace(options.Name))
                options.Name = "default";

            var options2 = configuration.GetValue<DapperOptions>($"Cosmos:Data:{options.Name}");
            options.MergeOptionsFrom(options2);

            if (options.Name == "default")
                options.Name = options.ConnectionString.GetHashCode().ToString();

            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        #endregion

        #region UseSqlServerDapperStrictly

        public static IDbContextConfig UseDapperWithSqlServerStrictly<TContext>(
            this IDbContextConfig config,
            string name,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            var options = new DapperOptions<TContext> {Name = name, ConnectionString = connectionString};
            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServerStrictly<TContext>(
            this IDbContextConfig config,
            string name,
            IConfiguration configuration,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(name))
                name = "default";

            var options = configuration.GetValue<DapperOptions<TContext>>($"Cosmos:Data:{name}");

            if (options.ConnectionString.IsNullOrWhiteSpace())
                options.ConnectionString = configuration.GetConnectionString(name);

            if (options.Name.IsNullOrWhiteSpace())
                options.Name = name == "default"
                    ? typeof(TContext).FullName
                    : name;

            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServerStrictly<TContext>(
            this IDbContextConfig config,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            var options = new DapperOptions<TContext> {Name = typeof(TContext).FullName, ConnectionString = connectionString};
            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServerStrictly<TContext>(
            this IDbContextConfig config,
            Action<DapperOptions> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            var options = new DapperOptions<TContext>();
            optionAct?.Invoke(options);
            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        public static IDbContextConfig UseDapperWithSqlServerStrictly<TContext>(
            this IDbContextConfig config,
            IConfiguration configuration,
            Action<DapperOptions> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = new DapperOptions<TContext>();
            optionAct?.Invoke(options);

            if (string.IsNullOrWhiteSpace(options.Name))
                options.Name = "default";

            var options2 = configuration.GetValue<DapperOptions<TContext>>($"Cosmos:Data:{options.Name}");
            options.MergeOptionsFrom(options2);

            if (options.Name == "default")
                options.Name = options.ConnectionString.GetHashCode().ToString();

            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        #endregion

        private static class CoreRegistrar
        {
            public static IDbContextConfig AsNormal(IDbContextConfig config, DapperOptions options,
                Action<FluentMapConfiguration> configureConvention = null,
                IList<Assembly> mappingAssemblies = null)
            {
                Register(options, false, configureConvention, mappingAssemblies);

                var optDescriptor = RegisterProxyDescriptor.Create<DapperOptions>(options, RegisterProxyLifetimeType.Singleton);

                config.Configure(bag => { bag.Register(optDescriptor); });

                DapperOptionManager.Set(options);

                return config;
            }

            public static IDbContextConfig AsStrictly<TContext>(IDbContextConfig config, DapperOptions<TContext> options,
                Action<FluentMapConfiguration> configureConvention = null,
                IList<Assembly> mappingAssemblies = null)
                where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
            {
                var mappingConfig = Register(options, true, configureConvention, mappingAssemblies);

                var context = BuildContextInstanceOnce<TContext>(options.ConnectionString);

                DapperContextualManager.ModelCreatingCall(context, mappingConfig);

                var ctxDescriptor = RegisterProxyDescriptor.Create(() => BuildContextInstanceOnce<TContext>(options.ConnectionString), RegisterProxyLifetimeType.Scoped);
                var optDescriptor = RegisterProxyDescriptor.Create<DapperOptions<TContext>>(options, RegisterProxyLifetimeType.Singleton);

                config.Configure(bag =>
                {
                    bag.Register(ctxDescriptor);
                    bag.Register(optDescriptor);
                });

                DapperOptionManager.Set(options);

                return config;
            }

            private static DapperConfig Register(DapperOptions options, bool strictMode,
                Action<FluentMapConfiguration> configureConvention = null, IList<Assembly> mappingAssemblies = null)
            {
                InternalDapperRegistrar.GuadDapperOptions(options);

                var dialect = new SqlServerDialect();
                var defaultMapper = typeof(AutoClassMap<>);
                ISqlKataCompilerCreator sqlKataCompiler = new SqlKataCompilerCreator<SqlServerCompiler>();
                var dapperConfig = new DapperConfig(defaultMapper, mappingAssemblies, dialect, sqlKataCompiler, options, strictMode);

                if (configureConvention != null)
                {
                    var conventionConfig = new FluentMapConfiguration(dapperConfig);
                    configureConvention(conventionConfig);
                }

                SyncBindingManager.Sync(dapperConfig);

                DapperConfigAccessor.RefreshCache(options.ConnectionString, dapperConfig);

                DapperGlobalRegistrar.RegisterForCosmosDapper();

                return dapperConfig;
            }

            private static TContext BuildContextInstanceOnce<TContext>(string connectionString)
                where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
            {
                var connection = new SqlConnection(connectionString);
                return Types.CreateInstance<TContext>(typeof(TContext), connection);
            }
        }

    }
}