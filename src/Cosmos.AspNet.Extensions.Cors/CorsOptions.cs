using System;
using System.Collections.Generic;

/*
 * 本代码根据 Mocrosoft.AspNetCore.Cors 改写
 * https://github.com/aspnet/CORS/blob/dev/src/Microsoft.AspNetCore.Cors/Infrastructure/CorsOptions.cs
 * */

namespace Cosmos.AspNet.Extensions
{
    /// <summary>
    /// CORS Options
    /// </summary>
    public partial class CorsOptions
    {
        private string _defaultPolicyName = "__DefaultCorsPolicy";
        private IDictionary<string, CorsPolicy> PolicyMap { get; } = new Dictionary<string, CorsPolicy>();

        /// <summary>
        /// Gets or sets default policy name
        /// </summary>
        public string DefaultPolicyName
        {
            get => _defaultPolicyName;
            set => _defaultPolicyName = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        /// <summary>
        /// add policy
        /// </summary>
        /// <param name="name"></param>
        /// <param name="policy"></param>
        public void AddPolicy(string name, CorsPolicy policy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            PolicyMap[name] = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        /// <summary>
        /// add policy
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configurePolicy"></param>
        public void AddPolicy(string name, Action<CorsPolicyBuilder> configurePolicy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (configurePolicy == null)
            {
                throw new ArgumentNullException(nameof(configurePolicy));
            }

            var policyBuilder = new CorsPolicyBuilder();
            configurePolicy(policyBuilder);
            PolicyMap[name] = policyBuilder.Build();
        }

        /// <summary>
        /// Get ploicy by PolicyName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CorsPolicy GetPolicy(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return PolicyMap.ContainsKey(name) ? PolicyMap[name] : null;
        }
    }
}
