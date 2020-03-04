using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomPolicyProvider
{
    public class MinAgeAuthorizationHandler : AuthorizationHandler<MinAgeRequirement>
    {
        public MinAgeAuthorizationHandler()
        {
            
        }

        // Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name)) {
                return Task.CompletedTask;
            }
            var name = context.User.FindFirst(u => u.Type == ClaimTypes.Name).Value;
            int age = 5;
            if (name.ToLower().Equals("alice")) {
                age = 20;
            }

            if (age >= requirement.Age) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}