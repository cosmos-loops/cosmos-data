using System.IO.Compression;
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
            var preferredEncoding = Internal.RequestHelper.GetPreferredEncoding(filterContext.HttpContext.Request);

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
    }
}
