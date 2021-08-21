/*
* A copy of https://github.com/2881099/dng.Mysql/blob/master/MySql.Data.MySqlClient/MySqlConnectionPool.cs
* Author: 2881099
* MIT
*/

using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cosmos.Data.Core.Pools;
using Cosmos.Disposables.ObjectPools;

namespace MySqlConnector
{
    /// <summary>
    /// MySql connection pool policy
    /// </summary>
    public class MySqlConnectionPoolPolicy : IConnectionPolicy<MySqlConnection>
    {
        // ReSharper disable once InconsistentNaming
        internal MySqlConnectionPool _pool;

        /// <inheritdoc />
        public string Name { get; set; } = "MySqlConnector MySqlConnection Object Pool";

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

                var m = Regex.Match(_connectionString, @"Max\s*pool\s*size\s*=\s*(\d+)", RegexOptions.IgnoreCase);

                if (m.Success == false
                 || int.TryParse(m.Groups[1].Value, out var poolSize) == false
                 || poolSize <= 0) poolSize = 100;
                PoolSize = poolSize;

                var initConnArray = new ObjectPayload<MySqlConnection>[poolSize];

                for (var a = 0; a < poolSize; a++)
                {
                    try
                    {
                        initConnArray[a] = _pool.Acquire();
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
        public MySqlConnection OnCreate() => new (_connectionString);

        /// <inheritdoc />
        public void OnAvailable() => _pool.AvailableHandler?.Invoke();

        /// <inheritdoc />
        public void OnUnavailable() => _pool.UnavailableHandler?.Invoke();

        /// <inheritdoc />
        public bool OnCheckAvailable(ObjectPayload<MySqlConnection> obj)
        {
            if (obj.Value.Ping() == false)
                obj.Value.Open();
            return obj.Value.Ping();
        }

        /// <inheritdoc />
        public void OnAcquire(ObjectPayload<MySqlConnection> obj)
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
        public async Task OnAcquireAsync(ObjectPayload<MySqlConnection> obj)
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
        public void OnRecycle(ObjectPayload<MySqlConnection> obj) { }

        /// <inheritdoc />
        public void OnDestroy(MySqlConnection obj)
        {
            if (obj.State != ConnectionState.Closed)
                obj.Close();
            obj.Dispose();
        }
    }
}