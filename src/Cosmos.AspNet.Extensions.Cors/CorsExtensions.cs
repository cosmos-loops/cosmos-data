using System;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// CORS Extensions
    /// </summary>
    public static class CorsExtensions
    {
        /// <summary>
        /// 全局使用 CORS
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseCors(this GlobalFilterCollection filters)
        {
            return UseCors(filters, null);
        }

        /// <summary>
        /// 全局使用 CORS
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseCors(this GlobalFilterCollection filters, Action<CorsOptions> optionsAction)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            var options = new CorsOptions();
            optionsAction?.Invoke(options);


            return filters;
        }
    }
}
