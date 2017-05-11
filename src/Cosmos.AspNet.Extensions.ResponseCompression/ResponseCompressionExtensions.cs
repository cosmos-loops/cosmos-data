using System;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// Response compression extensions
    /// </summary>
    public static class ResponseCompressionExtensions
    {
        /// <summary>
        /// 全局使用 Compression filter
        /// </summary>
        /// <param name="app"></param>
        public static HttpApplication UseResponseCompression(this HttpApplication app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var preferredEncoding = Internal.RequestHelper.GetPreferredEncoding(app.Request);

            if (preferredEncoding == CompressionScheme.Gzip)
            {
                app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (preferredEncoding == CompressionScheme.Deflate)
            {
                app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }

            return app;
        }

        /// <summary>
        /// 全局使用 Compression filter
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseResponseCompression(this GlobalFilterCollection filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            filters.Add(new CompressionAttribute());

            return filters;
        }
    }
}
