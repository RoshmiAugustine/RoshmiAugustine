using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.API.Filters.Policy
{
    public class APIUserPermissionRequirement : IAuthorizationRequirement
    {
        public APIUserPermissionRequirement()
        {
        }
    }
}
