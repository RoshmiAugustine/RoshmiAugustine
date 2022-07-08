// -----------------------------------------------------------------------
// <copyright file="IAssessmentService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IAssessmentService
    {
        /// <summary>
        /// GetQuestions.
        /// </summary>
        /// <param name="id">Questionnaire ID</param>
        /// <returns>QuestionsResponseDTO.</returns>
        QuestionsResponseDTO GetQuestions(int id);

        /// <summary>
        /// GetAssessmentDetails.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">ID of a Questionnaire.</param>
        /// <param name="date">date.</param>
        /// <param name="pageNumber"></param>
        /// <param name="totalCount"></param>
        /// <returns>AssessmentDetailsResponseDTO.</returns>
        AssessmentDetailsResponseDTO GetAssessmentDetails(Guid personIndex, int questionnaireId, DateTime? date, UserTokenDetails userTokenDetails,int pageNumber,long totalCount, int assessmentId);

        /// <summary>
        /// GetAssessmentValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <param name="assessmentIDs">Id of Assesments</param>
        /// <returns>AssessmentResponseDTO.</returns>
        AssessmentResponseDTO GetAssessmentValues(Guid personIndex, int questionnaireId, UserTokenDetails userTokenDetails, string assessmentIDs);

        /// <summary>
        /// GetQuestionnaireDefaultResponseValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <param name="agencyID">Id of a agency.</param>
        /// <returns>QuestionnaireDefaultResponseValuesDTO.</returns>
        QuestionnaireDefaultResponseValuesDTO GetQuestionnaireDefaultResponseValues(Guid personIndex, int questionnaireId, long agencyID);

        /// <summary>
        /// AddAssessmentProgress
        /// </summary>
        /// <param name="assessmentProgressData"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        AddAssessmentResponseDTO AddAssessmentProgress(List<AssessmentProgressInputDTO> assessmentProgressData, int updateUserID, bool IsEmailAssessment, long agencyID);

        /// <summary>
        /// SendAssessmentEmailLink.
        /// </summary>
        /// <param name="EmailLinkParameterDetails">EmailLinkParameterDetails.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="agencyAbbrev">agencyAbbrev.</param>
        /// <param name="isResend">isResend.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO SendAssessmentEmailLink(AssessmentEmailLinkInputDTO EmailLinkParameterDetails, long agencyID, string agencyAbbrev, bool isResend);

        /// <summary>
        /// RemoveAssessment.
        /// </summary>
        /// <param name="assessmentID">The assessmentID<see cref="int"/>.</param>
        /// <param name="roles">The roles<see cref="List<string>"/>.</param>
        /// <returns>CRUDResponseDTO.<ResultDTO>.</returns>
        CRUDResponseDTO RemoveAssessment(int assessmentID, List<string> roles, long agencyID);

        /// <summary>
        /// AddAssessmentForEmail
        /// </summary>
        /// <param name="assessmentData"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        AddAssessmentResponseDTO AddAssessmentForEmail(AssessmentInputDTO assessmentData, int updateUserID, long agencyID, string agencyAbbrev);

        /// <summary>
        ///  ResendAssessmentEmail.
        /// </summary>
        /// <param name="assessmentEmailInput">assessmentEmailInput.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="agencyAbbrev">agencyAbbrev.</param>
        /// <returns></returns>
        AddAssessmentResponseDTO ResendAssessmentEmail(AssessmentEmailLinkInputDTO assessmentEmailInput, long agencyID, string agencyAbbrev);

        /// <summary>
        /// AddNotificationOnAssessment
        /// </summary>
        /// <param name="assesmentNotificationInputDTO"></param>
        /// <returns>CRUDResponseDTO</returns>
        AddAssessmentResponseDTO AddNotificationOnAssessment(AssesmentNotificationInputDTO assesmentNotificationInputDTO, int? assessmentNoteID);

        /// <summary>
        /// DecryptAssessmentEmailLinkURL
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="encryptedURL"></param>
        /// <param name="agencyID"></param>
        /// <returns>AssessmentEmailLinkDetailsResponseDTO</returns>
        AssessmentEmailLinkDetailsResponseDTO GetDetailsFromAssessmentEmailLink(string otp, string encryptedURL, long agencyID);

        /// <summary>
        /// ChangeReviewStatus.
        /// </summary>
        /// <param name="assessmentReviewStatusData">assessmentReviewStatusData.</param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO ChangeReviewStatus(AssessmentReviewStatusDTO assessmentReviewStatusData, long agencyID);

        /// <summary>
        /// QuestionnaireByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>QuestionsResponseDTO.</returns>
        QuestionsResponseDTO QuestionnaireByAssessmentID(int assessmentID, long agencyID);

        /// <summary>
        /// AssessmentByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        AssessmentResponseDTO AssessmentByAssessmentID(int assessmentID, long agencyID);

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentPriorityInputDTO">assessmentPriorityInputDTO.</param>
        /// <param name="userID">userID</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AssessmentPriority(AssessmentPriorityInputDTO assessmentPriorityInputDTO, int userID);

        /// <summary>
        /// Send Email Assessment Otp
        /// </summary>
        /// <param name="encryptedURL"></param>
        /// <returns>AssessmentEmailOtpResponseDTO</returns>
        AssessmentEmailOtpResponseDTO SendEmailAssessmentOtp(string encryptedURL, long agencyID, string callFrom ="");

        /// <summary>
        /// Get Last Assessment ByPerson
        /// </summary>
        /// <param name="personIndex">personIndex</param>
        /// <param name="questionnaireID">questionnaireID</param>
        /// <returns></returns>
        LastAssessmentResponseDTO GetLastAssessmentByPerson(Guid personIndex, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, UserTokenDetails userTokenDetails);

        /// <summary>
        /// create agency base url
        /// </summary>
        /// <param name="agencyAbbrev"></param>
        /// <returns></returns>
        string CreateBaseURL(string agencyAbbrev);

        /// <summary>
        /// GetAssessmentById.
        /// </summary>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponseDetailsDTO.</returns>
        AssessmentResponsesDetailDTO GetAssessmentById(int assessmentId);

        /// <summary>
        /// GetAssessmentByPersonQuestionaireIdAndStatus.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <param name="status">status.</param>
        /// <returns>AssessmentDetailsDTO</returns>
        AssessmentResponsesDetailDTO GetAssessmentByPersonQuestionaireIdAndStatus(long personQuestionnaireId, string status);

        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="assessmentId">assessmentId</param>
        /// <returns>AssessmentResponsesDetailsDTO.</returns>
        AssessmentResponsesDetailsDTO GetAssessmentResponses(int assessmentId);
        /// <summary>
        /// BatchUploadAssessments.
        /// </summary>
        /// <param name="assessmentImportData"></param>
        /// <returns></returns>
        CRUDResponseDTO BatchUploadAssessments(UploadAssessmentDTO assessmentDataToUpload);

        /// <summary>
        /// GetAssessmentsByPersonQuestionaireID.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>AssessmentResponsesDetailDTO.</returns>
        AssessmentResponsesDetailDTO GetAssessmentsByPersonQuestionaireID(long personQuestionnaireId);

        /// <summary>
        /// GetAssessmentResponseFOrDashboardCalculation.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponsesDetailDTO.</returns>
        AssessmentResponsesDetailsDTO GetAssessmentResponseFOrDashboardCalculation(long personId, int assessmentId);

        /// <summary>
        /// GetQuestionnaireSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>SkipLogicResponseDetailsDTO.</returns>
        public SkipLogicResponseDetailsDTO GetQuestionnaireSkipLogic(int questionnaireId);
        /// <summary>
        /// GetAssessmentDetailsForEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDs">ehrAssessmentIDs.</param>
        /// <returns>EHRAssessmentResponseDTO.</returns>
        EHRAssessmentResponseDTO GetAssessmentDetailsForEHRUpdate(string ehrAssessmentIDs);
        /// <summary>
        /// GetAssessmentIDsForEHRUpdate.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>EHRAssessmentResponseDTO.</returns>
        EHRAssessmentResponseDTO GetAssessmentIDsForEHRUpdate(long agencyID);
        /// <summary>
        /// UpdateAssessmentFlagAfterEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDsList">ehrAssessmentIDsList.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateAssessmentFlagAfterEHRUpdate(List<int> ehrAssessmentIDsList);
        /// <summary>
        /// GetAssessmentDetailsListsForExternal.
        /// </summary>
        /// <param name="assessmentSearchInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        AssessmentDetailsResponseDTOForExternal GetAssessmentDetailsListsForExternal(AssessmentSearchInputDTO assessmentSearchInputDTO, LoggedInUserDTO loggedInUserDTO);
        /// <summary>
        /// GetReceiversDetailsForReminderInvite.
        /// </summary>
        /// <param name="personQuestionnaireIds"></param>
        /// <param name="typeOfInvite"></param>
        /// <returns></returns>
        InviteMailReceiversDetailsResponseDTO GetReceiversDetailsForReminderInvite(List<long> personQuestionnaireIds, string typeOfInvite);
        /// <summary>
        /// BulkAddAssessmentsForReminders.
        /// </summary>
        /// <param name="assessmentDataToUpload"></param>
        /// <returns></returns>
        BulkAddAssessmentResponseDTO BulkAddAssessmentsForReminders(AssessmentBulkAddOnInviteDTO assessmentDataToUpload);        
    }
}
