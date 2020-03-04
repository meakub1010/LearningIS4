using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomPolicyProvider.Policy
{
    public class ReadClaimsHandler : AuthorizationHandler<ReadClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadClaimsRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Value.Equals("afcpayroll"))) {
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

    }
}
