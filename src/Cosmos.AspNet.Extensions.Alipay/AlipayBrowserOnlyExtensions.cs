using System;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// aliapy browser only extensions
    /// </summary>
    public static class AlipayBrowserOnlyExtensions
    {
        /// <summary>
        /// 全局使用 Alipay Browser Only 过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseAlipayBrowserOnly(this GlobalFilterCollection filters)
        {
            return UseAlipayBrowserOnly(filters, null);
        }

        /// <summary>
        /// 全局使用 Alipay Browser Only 过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseAlipayBrowserOnly(this GlobalFilterCollection filters, Action<AlipayBrowserOnlyOptions> optionsAction)
        {
            var options = new AlipayBrowserOnlyOptions();
            optionsAction?.Invoke(options);

            filters.Add(Internal.AlipayBrowserOnlyAttributeFactory.Create(options));

            return filters;
        }
    }
}
