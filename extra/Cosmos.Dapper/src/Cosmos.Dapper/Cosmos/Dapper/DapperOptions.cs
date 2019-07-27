namespace Cosmos.Dapper
{
    public class DapperOptions
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public int? Timeout { get; set; }

        public int BatchSize { get; set; } = 500;

        public string SecureFilePriv { get; set; } = null;

        public void MergeOptionsFrom(DapperOptions options)
        {
            if (options == null)
                return;

            if (!string.IsNullOrWhiteSpace(options.ConnectionString))
                ConnectionString = options.ConnectionString;

            if (options.Timeout.HasValue && options.Timeout.Value > 0)
                Timeout = options.Timeout.Value;

            if (options.BatchSize >= 0)
                BatchSize = options.BatchSize;

            SecureFilePriv = options.SecureFilePriv;
        }
    }

    public class DapperOptions<TContext> : DapperOptions where TContext : class, IDapperContext { }
}