// -----------------------------------------------------------------------
// <copyright file="IHelperService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IConsumerService.
    /// </summary>
    public interface IHelperService
    {
        /// <summary>
        /// To save helper.
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns>Object of StatusResponse</returns>
        CRUDResponseDTO SaveHelperDetails(HelperDetailsDTO helperData, long agencyID);

        /// <summary>
        /// GetHelperDetails.
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns></returns>
        GetHelperResponseDTO GetHelperDetails(int userId, int pageNumber, int pageSize);

        /// <summary>
        /// Get helper details.
        /// </summary>
        /// <param name="helperIndex"></param>
        /// <returns>Helper details</returns>
        HelperViewResponseDTO GetHelperInfo(Guid helperIndex, long agencyID=0);

        /// <summary>
        /// to update helper.
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns>StatusResponseDTO</returns>
        Task<CRUDResponseDTO> UpdateHelperDetails(HelperDetailsDTO helperData, long agencyID);

        /// <summary>
        /// to remove helper.
        /// </summary>
        /// <param name="helperIndex"></param>
        /// <returns>StatusResponseDTO</returns>
        CRUDResponseDTO RemoveHelperDetails(Guid helperIndex, long agencyID);

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserDetailsResponseDTO.</returns>
        UserDetailsResponseDTO GetUserDetails(Int64 userID);


        /// <summary>
        /// GetHelperDetailsByHelperID.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="helperID">helperID.</param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="roles">roles</param>
        /// <returns></returns>
        GetHelperResponseDTO GetHelperDetailsByHelperID(HelperSearchDTO helperSearchDTO);

        /// <summary>
        /// Set Super Admin Default Agency
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        CRUDResponseDTO SetSuperAdminDefaultAgency(int userId, long agencyId);

        /// <summary>
        /// Reset Helper Password
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO ResendHelperPassword(Guid helperIndex, long agencyId);

        /// <summary>
        /// GetHelperDetailsByHelperEmail
        /// </summary>
        /// <param name="helperEmailCSV"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        HelperViewResponseDTO GetHelperDetailsByHelperEmail(string helperEmailCSV, long agencyID);

        /// <summary>
        /// ImportHelper.
        /// </summary>
        /// <param name="helperDetailsDTO">helperDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO ImportHelper(List<HelperDetailsDTO> helperDetailsDT, long agencyID);

        /// <summary>
        /// ValidateHelperEmail.
        /// </summary>
        /// <param name="jsonData">jsonData.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>EmailValidationResponseDTO.</returns>
        EmailValidationResponseDTO ValidateHelperEmail(string jsonData, long agencyID);

        /// <summary>
        /// GetAllHelperDetailsForExternal
        /// </summary>
        /// <param name="helperSearchInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns>GetHelperResponseDTOForExternal</returns>
        GetHelperResponseDTOForExternal GetAllHelperDetailsForExternal(HelperSearchInputDTO helperSearchInputDTO, LoggedInUserDTO loggedInUserDTO);

        /// <summary>
        /// SaveHelperDetailsForExternal
        /// </summary>
        /// <param name="helperInputData">helperInputData</param>
        /// <returns>CRUDResponseDTOForExternal</returns>
        CRUDResponseDTOForExternal SaveHelperDetailsForExternal(HelperDetailsInputDTO helperInputData);
       
        /// <summary>
        /// UpdateHelperDetailsForExternal
        /// </summary>
        /// <param name="helperInputData">helperInputData</param>
        /// <returns>CRUDResponseDTOForExternal</returns>
        Task<CRUDResponseDTOForExternal> UpdateHelperDetailsForExternal(HelperDetailsEditInputDTO helperInputData);
    }
}
