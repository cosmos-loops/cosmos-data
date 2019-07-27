using System;
using System.Collections.Concurrent;
using FreeSql;

namespace Cosmos.FreeSql.Core
{
    public class FreeSqlManager
    {
        private readonly ConcurrentDictionary<int, IFreeSql> _freeSqlDict;

        public FreeSqlManager()
        {
            _freeSqlDict = new ConcurrentDictionary<int, IFreeSql>();
        }

        public void Register(string connectionString, DataType dataType, Func<FreeSqlBuilder, FreeSqlBuilder> freeSqlBuilderFunc = null)
        {
            connectionString.CheckBlank(nameof(connectionString));
            var hash = connectionString.GetHashCode();

            if (_freeSqlDict.ContainsKey(hash))
                throw new InvalidOperationException($"Connection '{connectionString}' has been registered.");

            var builder = new FreeSqlBuilder().UseConnectionString(dataType, connectionString);
            builder = freeSqlBuilderFunc?.Invoke(builder) ?? builder;
            var freeSql = builder.Build();

            _freeSqlDict.TryAdd(hash, freeSql);
        }

        public bool ContainsConnection(string connectionString)
        {
            return !string.IsNullOrWhiteSpace(connectionString) && _freeSqlDict.ContainsKey(connectionString.GetHashCode());
        }

        public IFreeSql Get(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return null;

            return _freeSqlDict.TryGetValue(connectionString.GetHashCode(), out var freeSql) ? freeSql : null;
        }

        public IFreeSql Get(int hashOfConnectionString)
        {
            return _freeSqlDict.TryGetValue(hashOfConnectionString, out var freeSql) ? freeSql : null;
        }
    }
}