namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// Options for alipay browser only middleware
    /// </summary>
    public class WeChatBrowserOnlyOptions
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; } = "WeChat Browser Only";

        /// <summary>
        /// 302 跳转目标
        /// </summary>
        public string RedirectUrl { get; set; } = string.Empty;
    }
}
