// -----------------------------------------------------------------------
// <copyright file="IApplicationObjectTypeService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IApplicationObjectTypeService
    {
        Task<List<ApplicationObjectTypeDTO>> GetApplicationObjectTypes();
    }
}
