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
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, FrameOptionsConstants.DenyFrames);
                        break;
                    }

                case ResponseFramesOptionsType.SAMEORIGIN:
                    {
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, FrameOptionsConstants.SameOriginFrames);
                        break;
                    }

                case ResponseFramesOptionsType.ALLOWFROM when !string.IsNullOrWhiteSpace(domain):
                    {
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, $"{FrameOptionsConstants.AllowFrom} {domain}");
                        break;
                    }

                default:
                    {
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, FrameOptionsConstants.DenyFrames);
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
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, FrameOptionsConstants.DenyFrames);
                        break;
                    }

                case ResponseFramesOptionsType.SAMEORIGIN:
                    {
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, FrameOptionsConstants.SameOriginFrames);
                        break;
                    }

                case ResponseFramesOptionsType.ALLOWFROM when !string.IsNullOrWhiteSpace(domain):
                    {
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, $"{FrameOptionsConstants.AllowFrom} {domain}");
                        break;
                    }

                default:
                    {
                        response.AppendHeader(FrameOptionsConstants.XFramesOptions, FrameOptionsConstants.DenyFrames);
                        break;
                    }
            }
        }
    }
}
