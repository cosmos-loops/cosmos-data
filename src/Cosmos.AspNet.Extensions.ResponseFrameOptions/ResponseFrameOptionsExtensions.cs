using System;
using System.Web;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// Response XFrame-Options extensions
    /// </summary>
    public static class ResponseFrameOptionsExtensions
    {
        /// <summary>
        /// 全局使用 XFrame-Options
        /// </summary>
        /// <param name="app"></param>
        public static HttpApplication UseResponseFrameOptions(this HttpApplication app)
        {
            return UseResponseFrameOptions(app, ResponseFramesOptionsType.DENY, string.Empty);
        }

        /// <summary>
        /// 全局使用 XFrame-Options
        /// </summary>
        /// <param name="app"></param>
        /// <param name="type"></param>
        public static HttpApplication UseResponseFrameOptions(this HttpApplication app, ResponseFramesOptionsType type)
        {
            return UseResponseFrameOptions(app, type, string.Empty);
        }

        /// <summary>
        /// 全局使用 XFrame-Options
        /// </summary>
        /// <param name="app"></param>
        /// <param name="type"></param>
        /// <param name="domain"></param>
        public static HttpApplication UseResponseFrameOptions(this HttpApplication app, ResponseFramesOptionsType type, string domain)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            Internal.ResponseHelper.UpdateHeader(app.Response, type, domain);

            return app;
        }

        /// <summary>
        /// 全局使用 XFrame-Options
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseResponseFrameOptions(this GlobalFilterCollection filters)
        {
            return UseResponseFrameOptions(filters, ResponseFramesOptionsType.DENY, string.Empty);
        }

        /// <summary>
        /// 全局使用 XFrame-Options
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseResponseFrameOptions(this GlobalFilterCollection filters, ResponseFramesOptionsType type)
        {
            return UseResponseFrameOptions(filters, type, string.Empty);
        }

        /// <summary>
        /// 全局使用 XFrame-Options
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseResponseFrameOptions(this GlobalFilterCollection filters, ResponseFramesOptionsType type, string domain)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            filters.Add(new FrameOptionsAttribute { Domain = domain, FramesOptions = type });

            return filters;
        }
    }
}
