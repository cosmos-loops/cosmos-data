using System;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// WeChat browser only extensions
    /// </summary>
    public static class WeChatBrowserOnlyExtensions
    {
        /// <summary>
        /// 全局使用 WeChat Browser Only 过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseWeChatBrowserOnly(this GlobalFilterCollection filters)
        {
            return UseWeChatBrowserOnly(filters, null);
        }

        /// <summary>
        /// 全局使用 WeChat Browser Only 过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseWeChatBrowserOnly(this GlobalFilterCollection filters, Action<WeChatBrowserOnlyOptions> optionsAction)
        {
            var options = new WeChatBrowserOnlyOptions();
            optionsAction?.Invoke(options);

            filters.Add(Internal.WeChatBrowserOnlyAttributeFactory.Create(options));

            return filters;
        }
    }
}
