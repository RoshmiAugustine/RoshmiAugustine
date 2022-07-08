// -----------------------------------------------------------------------
// <copyright file="CustomCultureProvider.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Resources
{
    public class CustomCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            //Go away and do a bunch of work to find out what culture we should do. 
            await Task.Yield();
            string culture = "en-US";
            var claimsIdentity = (ClaimsIdentity)httpContext.User.Identity;
            var userIDclaim = claimsIdentity.FindFirst(PCISEnum.TokenClaims.Culture);
            if (userIDclaim != null && !String.IsNullOrEmpty(userIDclaim.Value))
            {
                culture = userIDclaim.Value;
            }
            else
            {
                culture = httpContext.Request.Headers[PCISEnum.TokenHeaders.Culture].ToString().Trim() == string.Empty ? culture : httpContext.Request.Headers[PCISEnum.TokenHeaders.Culture].ToString().Trim();
                //object cutureVal = String.Empty;
                //if (httpContext.Items.TryGetValue(PCISEnum.ConfigurationParameters.Culture, out object cutureVal))
                //{
                //    if (cutureVal != null)
                //    {
                //        culture = cutureVal.ToString();
                //    }
                //}

            }
            //Return a provider culture result. 
            return new ProviderCultureResult("en-US", culture);

            //In the event I can't work out what culture I should use. Return null. 
            //Code will fall to other providers in the list OR use the default. 
            //return null;
        }
    }
}