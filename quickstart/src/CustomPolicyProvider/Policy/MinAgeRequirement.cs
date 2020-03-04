using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPolicyProvider
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; private set; }
        public MinAgeRequirement(int age) {
            Age = age;
        }
    }
}
