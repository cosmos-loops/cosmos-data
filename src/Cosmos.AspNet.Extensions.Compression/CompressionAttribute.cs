using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// 压缩
    /// </summary>
    public class CompressionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// action executing...
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var preferredEncoding = GetPreferredEncoding(filterContext.HttpContext.Request);

            var response = filterContext.HttpContext.Response;
            response.AppendHeader("Content-Encoding", preferredEncoding.ToString());

            if (preferredEncoding == CompressionScheme.Gzip)
            {
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (preferredEncoding == CompressionScheme.Deflate)
            {
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }

        private CompressionScheme GetPreferredEncoding(HttpRequestBase request)
        {
            var acceptableEncoding = request.Headers["Accept-Encoding"].ToLower();
            if (string.IsNullOrEmpty(acceptableEncoding))
            {
                return CompressionScheme.Identity;
            }

            if (acceptableEncoding.Contains("gzip"))
            {
                return CompressionScheme.Gzip;
            }

            if (acceptableEncoding.Contains("deflate"))
            {
                return CompressionScheme.Deflate;
            }

            return CompressionScheme.Identity;
        }
    }
}
