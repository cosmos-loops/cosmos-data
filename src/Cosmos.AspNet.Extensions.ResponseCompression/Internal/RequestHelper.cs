using System.Web;

namespace Cosmos.AspNet.Extensions.Internal
{
    internal static class RequestHelper
    {
        public static CompressionScheme GetPreferredEncoding(HttpRequestBase request)
        {
            return GetCompressionScheme(request.Headers["Accept-Encoding"].ToLower());
        }

        public static CompressionScheme GetPreferredEncoding(HttpRequest request)
        {
            return GetCompressionScheme(request.Headers["Accept-Encoding"].ToLower());
        }

        private static CompressionScheme GetCompressionScheme(string acceptableEncoding)
        {
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
