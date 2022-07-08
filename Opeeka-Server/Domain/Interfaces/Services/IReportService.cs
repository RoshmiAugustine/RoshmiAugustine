// -----------------------------------------------------------------------
// <copyright file="IReportService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IReportService
    {
        /// <summary>
        /// GetItemReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>ItemDetailsResponseDTO.</returns>
        ItemDetailsResponseDTO GetItemReportData(ReportInputDTO reportInputDTO, UserTokenDetails userTokenDetails);
        /// <summary>
        /// GetStoryMapReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>StoryMapResponseDTO.</returns>
        StoryMapResponseDTO GetStoryMapReportData(ReportInputDTO reportInputDTO, UserTokenDetails userTokenDetails);
        /// <summary>
        /// GetPersonStrengthFamilyReportData.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>PersonStrengthReportResponseDTO.</returns>
        PersonStrengthReportResponseDTO GetPersonStrengthFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails);
        /// <summary>
        /// GetPersonNeedsFamilyReportData.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>PersonNeedReportResponseDTO.</returns>
        PersonNeedReportResponseDTO GetPersonNeedsFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails);
        /// <summary>
        /// GetSupportResourcesFamilyReportData.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>SupportResourceReportResponseDTO.</returns>
        SupportResourceReportResponseDTO GetSupportResourcesFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails);
        /// <summary>
        /// GetSupportNeedsFamilyReportData.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>SupportNeedsReportResponseDTO.</returns>
        SupportNeedsReportResponseDTO GetSupportNeedsFamilyReportData(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails);
        /// <summary>
        /// GetFamilyReportStatus.
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>FamilyReportStatusResponseDTO.</returns>
        FamilyReportStatusResponseDTO GetFamilyReportStatus(FamilyReportInputDTO familyReportInputDTO, UserTokenDetails userTokenDetails);

        /// <summary>
        /// HTMLToPDFByteArrayConversion 
        /// </summary>
        /// <param name="htmlData">htmlData</param>
        /// <returns>HTMLToPDFResponseDTO</returns>
        HTMLToPDFResponseDTO HTMLToPDFByteArrayConversion(PdfReportDTO htmlData);
        /// <summary>
        /// GetAllQuestionnairesForSuperStoryMap.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns></returns>
        AssessedQuestionnairesForPersonDTO GetAllQuestionnairesForSuperStoryMap(UserTokenDetails userTokenDetails, Guid personIndex, long personCollaborationID, int pageNumber, int pageSize);
        /// <summary>
        /// GetAllDetailsForSuperStoryMap.
        /// </summary>
        /// <param name="superStoryInputDTO">superStoryInputDTO.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        SuperStoryResponseDTO GetAllDetailsForSuperStoryMap(SuperStoryInputDTO superStoryInputDTO, long agencyID);
        /// <summary>
        /// GetAllPowerBIReportsForAgency.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="instrumentId"></param>
        /// <returns></returns>
        AgencyPowerBIReportResponseDTO GetAllPowerBIReportsForAgency(long agencyId, int instrumentId);
        /// <summary>
        /// GetPowerBIReportURLByReportID.
        /// </summary>
        /// <param name="powerBiInputDTO"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        PowerBIReportURLResponseDTO GetPowerBIReportURLByReportID(PowerBiInputDTO powerBiInputDTO, long agencyId, int userId);
    }
}
