using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPolicyProvider.Policy
{
    public class ReadClaimsRequirement : IAuthorizationRequirement
    {
        public string Scope { get;  }

        public ReadClaimsRequirement(string scope)
        {
            Scope = scope;
        }
    }
}
