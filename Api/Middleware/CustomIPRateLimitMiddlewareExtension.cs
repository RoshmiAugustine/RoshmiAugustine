using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Api.Middleware
{
    public static class CustomIPRateLimitMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomIPRateLimitMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomIPRateLimitMiddleware>();
        }
    }
}
