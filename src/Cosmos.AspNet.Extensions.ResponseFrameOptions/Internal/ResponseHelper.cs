using System.Web;

namespace Cosmos.AspNet.Extensions.Internal
{
    internal static class ResponseHelper
    {
        public static void UpdateHeader(HttpResponse response, ResponseFramesOptionsType type, string domain)
        {
            switch (type)
            {
                case ResponseFramesOptionsType.DENY:
                    {
                        response.AppendHeader("X-Frames-Options", "DENY");
                        break;
                    }

                case ResponseFramesOptionsType.SAMEORIGIN:
                    {
                        response.AppendHeader("X-Frames-Options", "SAMEORIGIN");
                        break;
                    }

                case ResponseFramesOptionsType.ALLOWFROM when !string.IsNullOrWhiteSpace(domain):
                    {
                        response.AppendHeader("X-Frames-Options", $"ALLOW-FROM {domain}");
                        break;
                    }

                default:
                    {
                        response.AppendHeader("X-Frames-Options", "DENY");
                        break;
                    }
            }
        }


        public static void UpdateHeader(HttpResponseBase response, ResponseFramesOptionsType type, string domain)
        {
            switch (type)
            {
                case ResponseFramesOptionsType.DENY:
                    {
                        response.AppendHeader("X-Frames-Options", "DENY");
                        break;
                    }

                case ResponseFramesOptionsType.SAMEORIGIN:
                    {
                        response.AppendHeader("X-Frames-Options", "SAMEORIGIN");
                        break;
                    }

                case ResponseFramesOptionsType.ALLOWFROM when !string.IsNullOrWhiteSpace(domain):
                    {
                        response.AppendHeader("X-Frames-Options", $"ALLOW-FROM {domain}");
                        break;
                    }

                default:
                    {
                        response.AppendHeader("X-Frames-Options", "DENY");
                        break;
                    }
            }
        }
    }
}
