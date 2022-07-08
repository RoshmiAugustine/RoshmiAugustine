// -----------------------------------------------------------------------
// <copyright file="RolesLookup.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.Identity;


namespace Opeeka.PICS.Domain.Entities
{
    public class RolesLookup : IdentityRole
    {
        public string CreatedBy { get; set; }
    }
}