using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Policy
{
    internal class MinAgePolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "MinimumAge";
        public DefaultAuthorizationPolicyProvider FallBackPolicyProvider { get;  }
        public MinAgePolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallBackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        //public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        //{
        //    return FallBackPolicyProvider.GetFallBackPolicyAsync();
        //}

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
               int.TryParse(policyName.Substring(POLICY_PREFIX.Length), out var age))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new MinAgeRequirement(age));
                return Task.FromResult(policy.Build());
            }

            return FallBackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
