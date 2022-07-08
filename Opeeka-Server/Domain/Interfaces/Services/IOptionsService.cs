// -----------------------------------------------------------------------
// <copyright file="IOptionsService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// ICollaborationLevelService
    /// </summary>
    public interface IOptionsService
    {
        /// <summary>
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        CollaborationLevelResponseDTO GetCollaborationLevelList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelData">collaborationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddCollaborationLevel(CollaborationLevelDTO collaborationLevelData, int userID, long agencyID);

        /// <summary>
        /// UpdateCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelData">collaborationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateCollaborationLevel(CollaborationLevelDTO collaborationLevelData, int userID, long agencyID);

        /// <summary>
        /// DeleteCollaborationLevel.
        /// </summary>
        /// <param name="collaborationlevelID">collaborationlevelID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteCollaborationLevel(int collaborationlevelID, long agencyID);

        /// <summary>
        /// Get the Collaboration Tag Type list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CollaborationTagTypeListResponseDTO</returns>
        CollaborationTagTypeListResponseDTO GetCollaborationTagTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Get the Collaboration Tag Type for an agency list
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CollaborationTagTypeListResponseDTO</returns>
        CollaborationTagTypeListResponseDTO GetCollaborationTagTypeList(int agencyId, int pageNumber, int pageSize);

        /// <summary>
        /// Add a new Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        CRUDResponseDTO AddCollaborationTagType(CollaborationTagTypeDetailsDTO collaborationTagTypeData, int userID, long agencyID);

        /// <summary>
        /// Update an existing Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO UpdateCollaborationTagType(CollaborationTagTypeDetailsDTO collaborationTagTypeData, int userID, long agencyID);

        /// <summary>
        /// Delete an existing Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeID"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO DeleteCollaborationTagType(int collaborationTagTypeID, long agencyID);

        /// <summary>
        /// Get the Helper title list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>TherapyTypesResponseDTO</returns>
        TherapyTypesResponseDTO GetTherapyTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Get the Helper title for an agency list
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>TherapyTypesResponseDTO</returns>
        TherapyTypesResponseDTO GetTherapyTypeList(int agencyId, int pageNumber, int pageSize);

        /// <summary>
        /// Add a new Helper title
        /// </summary>
        /// <param name="therapyTypeData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        CRUDResponseDTO AddTherapyType(TherapyTypeDetailsDTO therapyTypeData, int userID, long agencyID);

        /// <summary>
        /// Update an existing Helper title
        /// </summary>
        /// <param name="therapyTypeData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO UpdateTherapyType(TherapyTypeDetailsDTO therapyTypeData, int userID, long agencyID);

        /// <summary>
        /// Delete an existing Helper title
        /// </summary>
        /// <param name="therapyTypeID"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO DeleteTherapyType(int therapyTypeID, long agencyID);

        /// <summary>
        /// Get the Helper title list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>HelperTitleResponseDTO</returns>
        HelperTitleResponseDTO GetHelperTitleList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Get the Helper title for an agency list
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>HelperTitleResponseDTO</returns>
        HelperTitleResponseDTO GetHelperTitleList(long agencyId, int pageNumber, int pageSize);

        /// <summary>
        /// Add a new Helper title
        /// </summary>
        /// <param name="helperTitleData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        CRUDResponseDTO AddHelperTitle(HelperTitleDetailsDTO helperTitleData, int userID, long agencyID);

        /// <summary>
        /// Update an existing Helper title
        /// </summary>
        /// <param name="helperTitleData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO UpdateHelperTitle(HelperTitleDetailsDTO helperTitleData, int userID, long agencyID);

        /// <summary>
        /// Delete an existing Helper title
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO DeleteHelperTitle(int helperTitleID, long agencyID);
        /// <summary>
        /// GetNotificationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        NotificationLevelResponseDTO GetNotificationLevelList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelData">NotificationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddNotificationLevel(NotificationLevelDTO notificationLevelData, int userID, long agencyID);

        /// <summary>
        /// UpdateNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelData">NotificationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateNotificationLevel(NotificationLevelDTO notificationLevelData, int userID, long agencyID);

        /// <summary>
        /// DeleteNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelDataID">NotificationLevelID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteNotificationLevel(int notificationLevelDataID, long agencyID);

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>GenderResponseDTO.</returns>
        GenderResponseDTO GetGenderList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddGender.
        /// </summary>
        /// <param name="genderData">GenderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddGender(GenderDTO genderData, int userID, long agencyID);

        /// <summary>
        /// UpdateGender.
        /// </summary>
        /// <param name="genderData">GenderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateGender(GenderDTO genderData, int userID, long agencyID);

        /// <summary>
        /// DeleteGender.
        /// </summary>
        /// <param name="genderID">GenderID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteGender(int genderID, long agencyID);

        /// <summary>
        /// GetIdentificationTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>IdentificationTypeResponseDTO.</returns>
        IdentificationTypesResponseDTO GetIdentificationTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddIdentificationType.
        /// </summary>
        /// <param name="identificationTypeData">IdentificationTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddIdentificationType(IdentificationTypeDTO identificationTypeData, int userID, long agencyID);

        /// <summary>
        /// UpdateIdentificationType.
        /// </summary>
        /// <param name="identificationTypeData">IdentificationTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateIdentificationType(IdentificationTypeDTO identificationTypeData, int userID, long agencyID);

        /// <summary>
        /// DeleteIdentificationType.
        /// </summary>
        /// <param name="identificationTypeDataID">IdentificationTypeID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteIdentificationType(Int64 identificationTypeDataID, long agencyID);

        /// <summary>
        /// GetRaceEthnicityList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>RaceEthnicityResponseDTO.</returns>
        RaceEthnicityResponseDTO GetRaceEthnicityList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityData">RaceEthnicityData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddRaceEthnicity(RaceEthnicityDTO raceEthnicityData, int userID, long agencyID);

        /// <summary>
        /// UpdateRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityData">RaceEthnicityData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateRaceEthnicity(RaceEthnicityDTO raceEthnicityData, int userID, long agencyID);

        /// <summary>
        /// DeleteRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityDataID">RaceEthnicityID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteRaceEthnicity(Int64 raceEthnicityDataID, long agencyID);

        /// <summary>
        /// GetsupportTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>supportTypeResponseDTO.</returns>
        SupportTypeResponseDTO GetSupportTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddsupportType.
        /// </summary>
        /// <param name="supportTypeData">supportTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddSupportType(SupportTypeDTO supportTypeData, int userID, long agencyID);

        /// <summary>
        /// UpdatesupportType.
        /// </summary>
        /// <param name="supportTypeData">supportTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateSupportType(SupportTypeDTO supportTypeData, int userID, long agencyID);

        /// <summary>
        /// DeletesupportType.
        /// </summary>
        /// <param name="supportTypeDataID">supportTypeID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteSupportType(Int64 supportTypeDataID, long agencyID);

        /// <summary>
        /// UpdateSexuality.
        /// </summary>
        /// <param name="EditSexualityDTO,updateUserID,AgencyID">genderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateSexuality(EditSexualityDTO editSexualityDTO, int updateUserID, long AgencyID);
        /// <summary>
        /// RemoveSexuality.
        /// </summary>
        /// <param name="SexualityID">SexualityID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO RemoveSexuality(int SexualityID, long agencyID);

        /// <summary>
        /// AddSexuality
        /// </summary>
        /// <param name="sexualityData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO AddSexuality(SexualityInputDTO sexualityData, long agencyID, int updateUserID);

        /// <summary>
        /// GetSexualityList
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>SexualityResponseDTO</returns>
        SexualityResponseDTO GetSexualityList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// AddIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderData">identifiedGenderData.</param>
        /// <param name="userID">userID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        CRUDResponseDTO AddIdentifiedGender(IdentifiedGenderDTO identifiedGenderData, int userID, long agencyID);

        /// <summary>
        /// UpdateIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderData">identifiedGenderData.</param>
        /// <param name="userID">userID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        CRUDResponseDTO UpdateIdentifiedGender(IdentifiedGenderDTO identifiedGenderData, int userID, long agencyID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        IdentifiedGenderResponseDTO GetIdentifiedGenderList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Delete an Identified Gender.
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO DeleteIdentifiedGender(int identifiedGenderID, long agencyID);
    }
}
