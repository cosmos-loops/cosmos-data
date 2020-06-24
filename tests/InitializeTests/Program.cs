using System;
using System.Threading;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.Extensions.DependencyInjection;
using Cosmos.Data.Common;
using Cosmos.Data.Common.Transaction;
using Cosmos.Data.Common.UnitOfWork;
using Cosmos.Disposables;
using Cosmos.IdUtils;
using Microsoft.Extensions.DependencyInjection;

namespace InitializeTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddScoped<ITraceIdMaker, DefaultTraceIdMaker>();
            services.AddScoped<TraceIdAccessor>();
            services.AddSingleton(RepositoryManager.Instance);
            services.AddScoped<ScopedRepositoryManager>();
            services.AddScoped<ITransactionManager, ScopedTransactionManager>();
            services.AddScoped<UnitOfWorkManager>();

            services.AddScoped<DbContext>();

            var reflector = RepositoryReflector.Create<IDbRepository>();
            RepositoryManager.Instance.Register(reflector);
            services.AddScoped(reflector.ServiceType, reflector.ImplementType);

            //services.ConfigureDynamicProxy(c=>c.)

            var provider = services.BuildDynamicProxyProvider();

            using (var scope = provider.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                Console.WriteLine("Start to load repository");
                // get repository
                var repository = scopedProvider.Resolve<IDbRepository>();
                Console.WriteLine("Load repository finished.");

                Console.WriteLine("Start to get trace id.");
                var id = repository.CurrentTraceId;

                Console.WriteLine("Got trace id.");
                Console.WriteLine(id);

                Console.WriteLine("Start to load RawTypedContext");
                Console.WriteLine(repository.RawTypedContext222 is null ? "CTX NULL" : "CTX NOT NULL");
                Console.WriteLine("Load RawTypedContext finished.");

                Console.WriteLine(repository.UnitOfWork is null ? "UOW NULL" : "UOW NOT NULL");

                // returns ok
            }

            Console.WriteLine("Hello World!");
        }
    }

    public class DbContext : IStoreContext
    {
        public DbContext()
        {
            Console.WriteLine("DbContext create");
        }

        public void Commit()
        {
            Console.WriteLine("Commit");
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Commit async");
            return Task.CompletedTask;
        }

        public void Rollback()
        {
            Console.WriteLine("Rollback");
        }
    }

    [Repository("Demo", typeof(DbRepository))]
    public interface IDbRepository : /*global::Cosmos.Data.Common.*/IRepository<DbContext> { }

    [RepositoryRawContext(typeof(DbContext), "RawTypedContext222")]
    public interface IRepository<T> : IRepository
    {
        T RawTypedContext222 { get; set; }
    }

    public abstract class RepositoryBase : DisposableObjects, IDbRepository
    {
        public string CurrentTraceId { get; set; }

        public IUnitOfWorkEntry UnitOfWork { get; set; }
        public DbContext RawTypedContext222 { get; set; }
    }

    public class DbRepository : RepositoryBase
    {
        public DbRepository()
        {
            Console.WriteLine("DbRepository creating");

            AddDisposableAction("dis", () => { Console.WriteLine("DbRepository disposing"); });
        }
    }
}