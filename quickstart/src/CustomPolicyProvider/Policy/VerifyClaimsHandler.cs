using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomPolicyProvider.Policy
{
    public class VerifyClaimsHandler : AuthorizationHandler<VerifyClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, VerifyClaimsRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Value.Equals("afcpayroll"))) {
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

    }
}
