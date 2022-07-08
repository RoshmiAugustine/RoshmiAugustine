// -----------------------------------------------------------------------
// <copyright file="IEmailDetailsRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IEmailDetailRepository
    {

        /// <summary>
        /// GetEmailDetails.
        /// </summary>
        /// <returns>EmailDetailsResponseDTO.</returns>
        List<EmailDetailsDTO> GetEmailDetails();
        /// <summary>
        /// UpdateEmailDetails.
        /// </summary>
        /// <param name="EmailDetails">EmailDetails.</param>
        /// <returns>EmailDetails list.</returns>
        List<EmailDetail> UpdateEmailDetails(List<EmailDetail> EmailDetails);

        /// <summary>
        /// AddEmailDetails
        /// </summary>
        /// <param name="EmailDetails"></param>
        /// <returns>EmailDetails</returns>
        void AddEmailDetails(List<EmailDetail> EmailDetails);
    }
}
