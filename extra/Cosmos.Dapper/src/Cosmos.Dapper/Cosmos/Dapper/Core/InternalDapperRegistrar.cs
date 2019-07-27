using System;

namespace Cosmos.Dapper.Core
{
    public static class InternalDapperRegistrar
    {
        public static void GuadDapperOptions(DapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.ConnectionString))
                throw new ArgumentNullException(nameof(options.ConnectionString));
        }
    }
}