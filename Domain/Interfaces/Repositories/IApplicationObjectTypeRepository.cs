// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IApplicationObjectTypeRepository : IAsyncRepository<ApplicationObjectType>
    {


        /// <summary>
        /// To get application object types
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UsersDTO</returns>
        Task<List<ApplicationObjectTypeDTO>> GetApplicationObjectTypes();

    }
}
