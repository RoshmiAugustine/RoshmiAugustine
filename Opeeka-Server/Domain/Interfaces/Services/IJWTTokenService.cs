// -----------------------------------------------------------------------
// <copyright file="IJWTTokenService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using System.Security.Claims;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IJWTTokenService
    {
        string GenerateJSONWebToken(UsersDTO userInfo);
        ClaimsPrincipal GetPrincipal(string token, string tenant, int agencyId, string personIndex, string collaborationSharingIndex, string agencySharingIndex, bool isActivePerson);
    }
}
