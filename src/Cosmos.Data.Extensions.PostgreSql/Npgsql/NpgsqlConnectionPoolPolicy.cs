/*
* A copy of https://github.com/2881099/dng.Pgsql/blob/master/Npgsql/NpgsqlConnectionPool.cs
* Author: 2881099
* MIT
*/

using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;

namespace Npgsql
{
    /// <summary>
    /// Npgsql connection pool policy
    /// </summary>
    public class NpgsqlConnectionPoolPolicy : IConnectionPolicy<NpgsqlConnection>
    {
        // ReSharper disable once InconsistentNaming
        internal NpgsqlConnectionPool _pool;

        /// <inheritdoc />
        public string Name { get; set; } = "PostgreSQL NpgsqlConnection Object Pool";

        /// <inheritdoc />
        public int PoolSize { get; set; } = 100;

        /// <inheritdoc />
        public TimeSpan SyncGetTimeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <inheritdoc />
        public TimeSpan IdleTimeout { get; set; } = TimeSpan.Zero;

        /// <inheritdoc />
        public int AsyncGetCapacity { get; set; } = 10000;

        /// <inheritdoc />
        public bool IsThrowGetTimeoutException { get; set; } = true;

        /// <inheritdoc />
        public bool IsAutoDisposeWithSystem { get; set; }

        /// <inheritdoc />
        public int CheckAvailableInterval { get; set; } = 5;

        private string _connectionString;

        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value ?? "";

                var m = Regex.Match(_connectionString, @"Maximum\s*pool\s*size\s*=\s*(\d+)", RegexOptions.IgnoreCase);

                if (m.Success == false
                 || int.TryParse(m.Groups[1].Value, out var poolSize) == false
                 || poolSize <= 0) poolSize = 100;
                PoolSize = poolSize;

                var initConnArray = new ObjectPayload<NpgsqlConnection>[poolSize];

                for (var i = 0; i < poolSize; i++)
                {
                    try
                    {
                        initConnArray[i] = _pool.Acquire();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                foreach (var conn in initConnArray)
                    _pool.Recycle(conn);
            }
        }

        /// <inheritdoc />
        public NpgsqlConnection OnCreate() => new (_connectionString);

        /// <inheritdoc />
        public void OnAvailable() => _pool.AvailableHandler?.Invoke();

        /// <inheritdoc />
        public void OnUnavailable() => _pool.UnavailableHandler?.Invoke();

        /// <inheritdoc />
        public bool OnCheckAvailable(ObjectPayload<NpgsqlConnection> obj)
        {
            if (obj.Value.State == ConnectionState.Closed)
                obj.Value.Open();
            var cmd = obj.Value.CreateCommand();
            cmd.CommandText = "select 1";
            cmd.ExecuteNonQuery();
            return true;
        }

        /// <inheritdoc />
        public void OnAcquire(ObjectPayload<NpgsqlConnection> obj)
        {
            if (_pool.IsAvailable)
            {
                if (obj.Value.State != ConnectionState.Open
                 || DateTime.Now.Subtract(obj.LastRecycledTime).TotalSeconds > 60 && obj.Value.Ping() == false)
                {
                    try
                    {
                        obj.Value.Open();
                    }
                    catch (Exception ex)
                    {
                        if (_pool.SetUnavailable(ex))
                            throw new Exception($"【{Name}】状态不可用，等待后台检查程序恢复方可使用。{ex.Message}");
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task OnAcquireAsync(ObjectPayload<NpgsqlConnection> obj)
        {
            if (_pool.IsAvailable)
            {
                if (obj.Value.State != ConnectionState.Open
                 || DateTime.Now.Subtract(obj.LastRecycledTime).TotalSeconds > 60 && obj.Value.Ping() == false)
                {
                    try
                    {
                        await obj.Value.OpenAsync();
                    }
                    catch (Exception ex)
                    {
                        if (_pool.SetUnavailable(ex))
                            throw new Exception($"【{Name}】状态不可用，等待后台检查程序恢复方可使用。{ex.Message}");
                    }
                }
            }
        }

        /// <inheritdoc />
        public void OnAcquireTimeout() { }

        /// <inheritdoc />
        public void OnRecycle(ObjectPayload<NpgsqlConnection> obj) { }

        /// <inheritdoc />
        public void OnDestroy(NpgsqlConnection obj)
        {
            if (obj.State != ConnectionState.Closed)
                obj.Close();
            obj.Dispose();
        }
    }
}