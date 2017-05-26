using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Ganss.XSS;

namespace Cosmos.AspNet.Extensions.Internal
{
    internal static class AntiXssCoreHelper
    {
        private static AntiXssOptions _options;

        public static void Init(AntiXssOptions options)
        {
            _options = options;
            InternalAntiXssManager.SetPolicyMap(options);
        }

        public static void ApplyPolicy(string policyName, HttpRequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var policy = _options.GetPolicy(policyName);

            ApplyPolicy(policy, request);
        }

        public static void ApplyPolicy(AntiXssPolicy policy, HttpRequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            RefreshRequestParams(request.Headers, policy);
            RefreshRequestParams(request.QueryString, policy);
            RefreshRequestParams(request.Form, policy);
        }

        private static void RefreshRequestParams(NameValueCollection coll, AntiXssPolicy policy)
        {
            if (coll == null || policy == null) { return; }

            var cachedDict = __sanitizer(coll, policy);
            __returnReq(cachedDict, coll);

            Dictionary<string, string> __sanitizer(NameValueCollection __coll, AntiXssPolicy __policy)
            {
                if (__coll == null)
                {
                    throw new ArgumentNullException(nameof(__coll));
                }

                if (__policy == null)
                {
                    throw new ArgumentNullException(nameof(__policy));
                }

                var sanitizer = GetSanitizer(__policy);
                var ret = new Dictionary<string, string>();
                foreach (var key in __coll.AllKeys)
                {
                    try
                    {
                        ret.Add(key, sanitizer.Sanitize(__coll[key], __policy.BaseUrl, __policy.OutputFormatter));
                    }
                    catch
                    {
                        ret.Add(key, __coll[key]);
                    }
                }

                return ret;
            }
            void __returnReq(Dictionary<string, string> __cachedDict, NameValueCollection __coll)
            {
                foreach (var key in __cachedDict.Keys)
                {
                    __coll[key] = __cachedDict[key];
                }
            }
        }

        internal static HtmlSanitizer GetSanitizer(AntiXssPolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            return new HtmlSanitizer(policy.AllowedTags, policy.AllowedSchemes, policy.AllowedAttributes, policy.UriAttributes, policy.AllowedCssProperties);
        }
    }
}
