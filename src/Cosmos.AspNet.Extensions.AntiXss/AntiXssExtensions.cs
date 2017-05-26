using System;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// AntiXss Extensions
    /// </summary>
    public static class AntiXssExtensions
    {
        /// <summary>
        /// 全局使用 AntiXss
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseCors(this GlobalFilterCollection filters)
        {
            return UseCors(filters, null);
        }

        /// <summary>
        /// 全局使用 AntiXss
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static GlobalFilterCollection UseCors(this GlobalFilterCollection filters, Action<AntiXssOptions> optionsAction)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            var options = new AntiXssOptions();
            optionsAction?.Invoke(options);

            //CorsCoreHelper.Init(options);

            return filters;
        }
    }
}
