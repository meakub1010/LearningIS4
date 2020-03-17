using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPolicyProvider.Policy
{
    public class VerifyClaimsRequirement : IAuthorizationRequirement
    {
        public string Scope { get;  }

        public VerifyClaimsRequirement()
        {
            //Scope = scope;
        }
    }
}
