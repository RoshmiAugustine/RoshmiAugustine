// -----------------------------------------------------------------------
// <copyright file="AssessmentController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// AssessmentController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class AssessmentController : BaseController
    {
        /// Initializes a new instance of the <see cref="assessmentService"/> class.
        private readonly IAssessmentService assessmentService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<AssessmentController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="assessmentService">assessmentService.</param>
        public AssessmentController(ILogger<AssessmentController> logger, IAssessmentService assessmentService)
        {
            this.assessmentService = assessmentService;
            this.logger = logger;
        }

        /// <summary>
        /// GetQuestions.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionsResponseDTO.</returns>
        [HttpGet]
        [Route("questions/{id}")]
        public ActionResult<QuestionsResponseDTO> GetQuestions(int id)
        {
            try
            {
                var response = this.assessmentService.GetQuestions(id);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestions/GET : Getting item : Exception  : Exception occurred getting Questions.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Questions. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentDetails.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">ID of a Questionnaire.</param>
        /// <param name="date">date.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="totalCount">totalCount.</param>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("assessment-details/{personIndex}/{questionnaireId}/{assessmentId}/{pageNumber}/{totalCount}/{date?}")]
        public ActionResult<AssessmentDetailsResponseDTO> GetAssessmentDetails(Guid personIndex, int questionnaireId, int assessmentId, int pageNumber, long totalCount, DateTime? date)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.assessmentService.GetAssessmentDetails(personIndex, questionnaireId, date, userTokenDetails, pageNumber, totalCount, assessmentId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentDetails/GET : Getting item : Exception  : Exception occurred getting Assessment Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentValues.
        /// </summary>
        /// <param name="assessmentvaluesData">parameters.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        [HttpPost]
        [Route("assessment-values")]
        public ActionResult<AssessmentResponseDTO> GetAssessmentValues([FromBody] AssesmentValuesInptDTO assessmentvaluesData)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.assessmentService.GetAssessmentValues(assessmentvaluesData.personIndex, assessmentvaluesData.questionnaireId,userTokenDetails,assessmentvaluesData.AssessmentIDs);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentValues/GET : Getting item : Exception  : Exception occurred getting Assessment Values. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Assessment Values. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireDefaultResponseValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <returns>QuestionnaireDefaultResponseValuesDTO.</returns>
        [HttpGet]
        [Route("questionnaire-default-response-values/{personIndex}/{questionnaireId}")]
        public ActionResult<QuestionnaireDefaultResponseValuesDTO> GetQuestionnaireDefaultResponseValues(Guid personIndex, int questionnaireId)
        {
            try
            {
                var agencyID = this.GetTenantID();
                var response = this.assessmentService.GetQuestionnaireDefaultResponseValues(personIndex, questionnaireId, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireDefaultResponseValues/GET : Getting item : Exception  : Exception occurred GetQuestionnaireDefaultResponseValues. {ex.Message}");
                return this.HandleException(ex, "An error occurred GetQuestionnaireDefaultResponseValues . Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddAssessmentProgress.
        /// </summary>
        /// <param name="assessmentProgressData">assessmentProgressData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        //[ServiceFilter(typeof(TransactionFilter))]
        [Route("assessment")]
        public ActionResult<AddAssessmentResponseDTO> AddAssessmentProgress([FromBody] List<AssessmentProgressInputDTO> assessmentProgressData)
        {
            try
            {
                if (assessmentProgressData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        int updateUserID = 0;
                        updateUserID = this.GetUserID();
                        long agencyID = this.GetTenantID();

                        var response = this.assessmentService.AddAssessmentProgress(assessmentProgressData, updateUserID, false, agencyID);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddAssessmentProgress/Post : Adding Assessment : Exception  : Exception occurred adding assessment progress. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding aassessment progress. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// RemoveAssessment.
        /// </summary>
        /// <param name="assessmentID">The assessmentID<see cref="int"/>.</param>
        /// <returns>ActionResult.<ResultDTO>.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpDelete]
        [Route("remove-assessment/{assessmentID}")]
        public ActionResult<CRUDResponseDTO> RemoveAssessment(int assessmentID)
        {
            try
            {
                var roles = this.GetRole();
                var agencyID = this.GetTenantID();
                CRUDResponseDTO returnData = this.assessmentService.RemoveAssessment(assessmentID, roles, agencyID);
                return returnData;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"RemoveAssessment/DELETE : Delete item : Exception  : Exception occurred Deleting assessment. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting assessment. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddAssessmentForEmail.
        /// </summary>
        /// <param name="assessmentData">assessmentData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpPost]
        [Route("email-assessment")]
        public ActionResult<AddAssessmentResponseDTO> AddAssessmentForEmail([FromBody] AssessmentInputDTO assessmentData)
        {
            try
            {
                if (assessmentData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        int updateUserID = 0;
                        var userID = this.GetUserID();
                        updateUserID = Convert.ToInt32(userID);
                        var agencyAbbrev = this.GetTenantAbbreviation();
                        var agencyID = this.GetTenantID();
                        var response = this.assessmentService.AddAssessmentForEmail(assessmentData, updateUserID, agencyID, agencyAbbrev);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddAssessmentForEmail/Post : Adding Assessment : Exception  : Exception occurred adding assessment for email. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding assessment for email. Please try again later or contact support.");
            }
        }

        /// <summary>
        ///  ResendAssessmentEmail.
        /// </summary>
        /// <param name="assessmentEmailInput">assessmentEmailInput.</param>
        /// <returns>AddAssessmentResponseDTO.</returns>
        [HttpPost]
        [Route("resend-email-assessment")]
        public ActionResult<AddAssessmentResponseDTO> ResendAssessmentEmail([FromBody] AssessmentEmailLinkInputDTO assessmentEmailInput)
        {
            try
            {
                if (assessmentEmailInput != null)
                {
                        var userID = this.GetUserID();
                        assessmentEmailInput.UpdateUserID = Convert.ToInt32(userID);
                        var agencyAbbrev = this.GetTenantAbbreviation();
                        var agencyID = this.GetTenantID();
                        var response = this.assessmentService.ResendAssessmentEmail(assessmentEmailInput, agencyID, agencyAbbrev);
                        return response;
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ResendAssessmentEmail/Post : Resend Email Assessment : Exception  : Exception occurred while resending email assessment. {ex.Message}");
                return this.HandleException(ex, "An error occurred during email assessment resend. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionsAnonymously.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionsResponseDTO.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("questions-anonymous/{id}")]
        public ActionResult<QuestionsResponseDTO> GetQuestionsAnonymously(int id)
        {
            try
            {
                var response = this.assessmentService.GetQuestions(id);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionsAnonymously/GET : Getting item : Exception  : Exception occurred getting Questions.{ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Questions. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddAssessmentProgressAnonymously.
        /// </summary>
        /// <param name="assessmentProgressAnonymousData">assessmentProgressAnonymousData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpPost]
        [AllowAnonymous]
        [Route("assessment-anonymous")]
        public ActionResult<AddAssessmentResponseDTO> AddAssessmentProgressAnonymously([FromBody] AssessmentProgressAnonymousInputDTO assessmentProgressAnonymousData)
        {
            try
            {
                if (assessmentProgressAnonymousData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.assessmentService.AddAssessmentProgress(assessmentProgressAnonymousData.assessmentProgressData, assessmentProgressAnonymousData.UpdateUserID, true, 0);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddAssessmentProgressAnonymously/Post : Adding Assessment progress : Exception  : Exception occurred while adding assessment progress. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding assessment progress. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DecryptAssessmentEmailLink.
        /// </summary>
        /// <param name="assessmentEmailOtpVerificationDTO">assessmentEmailOtpVerificationDTO.</param>
        /// <returns>AssessmentEmailLinkDetailsResponseDTO.</returns>
        [HttpPost]
        [Route("assessmentemail-otp-verification")]
        [AllowAnonymous]
        public ActionResult<AssessmentEmailLinkDetailsResponseDTO> DecryptAssessmentEmailLink(AssessmentEmailOtpVerificationDTO assessmentEmailOtpVerificationDTO)
        {
            try
            {
                if (assessmentEmailOtpVerificationDTO != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var agencyID = this.GetTenantID();
                        return this.assessmentService.GetDetailsFromAssessmentEmailLink(assessmentEmailOtpVerificationDTO.otp, assessmentEmailOtpVerificationDTO.decryptUrl, agencyID);
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"DecryptAssessmentEmailLink/Post : Decrypt Assessment Email Link : Exception  : Exception occurred during decrypting assessment email link. {ex.Message}");
                return this.HandleException(ex, "An error occurred during decrypting assessment email link. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ChangeReviewStatus.
        /// </summary>
        /// <param name="assessmentReviewStatusData">assessmentReviewStatusData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("assessment-review-status")]
        public ActionResult<CRUDResponseDTO> ChangeReviewStatus([FromBody] AssessmentReviewStatusDTO assessmentReviewStatusData)
        {
            try
            {
                if (assessmentReviewStatusData != null)
                {
                    if (assessmentReviewStatusData.ReviewUserID == 0)
                    {
                        assessmentReviewStatusData.ReviewUserID = this.GetUserID();
                    }

                    var response = this.assessmentService.ChangeReviewStatus(assessmentReviewStatusData, this.GetTenantID());
                    return response;
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ChangeReviewStatus/Post :   Assessment Review Status : Exception  : Exception occurred while changing assessment review status. {ex.Message}");
                return this.HandleException(ex, "An error occurred  while changing assessment review status. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// QuestionnaireByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>QuestionsResponseDTO.</returns>
        [HttpGet]
        [Route("questions-reviewer/{assessmentID}")]
        public ActionResult<QuestionsResponseDTO> QuestionnaireByAssessmentID(int assessmentID)
        {
            try
            {
                var response = this.assessmentService.QuestionnaireByAssessmentID(assessmentID, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"QuestionnaireByAssessmentID/Get :   Questionnaire By AssessmentID : Exception  : Exception occurred getting questionnaire by assessment id. {ex.Message}");
                return this.HandleException(ex, "An error occurred  while fetching Questionnaire By AssessmentID. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AssessmentByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        [HttpGet]
        [Route("assessment-reviewer/{assessmentID}")]
        public ActionResult<AssessmentResponseDTO> AssessmentByAssessmentID(int assessmentID)
        {
            try
            {
                var response = this.assessmentService.AssessmentByAssessmentID(assessmentID, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AssessmentByAssessmentID/Get :   Assessment By AssessmentID : Exception  : Exception occurred getting assessment by assessmentId. {ex.Message}");
                return this.HandleException(ex, "An error occurred  while fetching Assessment By AssessmentID. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentResponseInputDTO">assessmentResponseInputDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("assessment-priority")]
        public ActionResult<CRUDResponseDTO> AssessmentPriority([FromBody] AssessmentPriorityInputDTO assessmentResponseInputDTO)
        {
            try
            {
                var response = this.assessmentService.AssessmentPriority(assessmentResponseInputDTO, this.GetUserID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"AssessmentPriority/Put :Updating Assessment priority : Exception  : Exception occurred updating Assessment Priority. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating Assessment Priority. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// SendEmailAssessmentOtp.
        /// </summary>
        /// <param name="encryptedQueryParameter">encryptedQueryParameter.</param>
        /// <returns>AssessmentEmailOtpResponseDTO.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("email-assessment-otp-onload")]
        public ActionResult<AssessmentEmailOtpResponseDTO> SendEmailAssessmentOtp([FromBody] string encryptedQueryParameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(encryptedQueryParameter))
                {
                    if (this.ModelState.IsValid)
                    {
                        this.logger.LogInformation(MyLogEvents.TestItem, "OTP-Call initiated");
                        var agencyID = this.GetTenantID();
                        var result = this.assessmentService.SendEmailAssessmentOtp(encryptedQueryParameter, agencyID);
                        this.logger.LogInformation(MyLogEvents.TestItem, $"OTP-Call End..{result.AssessmentEmailOtpID}-{result.IsOTPVerificationNeeded}");
                        return result;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"SendEmailAssessmentOtp/Post : SendEmailAssessmentOtp : Exception  : Exception occurred during sending email assessment otp. {ex.Message}");
                return this.HandleException(ex, "An error occurred during sending email assessment otp. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// SendEmailAssessmentOtp.
        /// </summary>
        /// <param name="encryptedQueryParameter">encryptedQueryParameter.</param>
        /// <returns>AssessmentEmailOtpResponseDTO.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("email-assessment-otp")]
        public ActionResult<AssessmentEmailOtpResponseDTO> ReSendEmailAssessmentOtp([FromBody] string encryptedQueryParameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(encryptedQueryParameter))
                {
                    if (this.ModelState.IsValid)
                    {
                        this.logger.LogInformation(MyLogEvents.TestItem, "OTP-Call initiated");
                        var agencyID = this.GetTenantID();
                        var result = this.assessmentService.SendEmailAssessmentOtp(encryptedQueryParameter, agencyID, "Resend");
                        this.logger.LogInformation(MyLogEvents.TestItem, $"OTP-Call End..{result.AssessmentEmailOtpID}-{result.IsOTPVerificationNeeded}");
                        return result;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"SendEmailAssessmentOtp/Post : SendEmailAssessmentOtp : Exception  : Exception occurred during sending email assessment otp. {ex.Message}");
                return this.HandleException(ex, "An error occurred during sending email assessment otp. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get Last Assessment ByPerson.
        /// </summary>
        /// <returns>LastAssessmentResponseDTO.</returns>
        [HttpPost]
        [Route("latest-submitted-assessment")]
        public ActionResult<LastAssessmentResponseDTO> GetLastAssessmentByPerson(ReportInputDTO reportInputDTO)
        {
            try
            {
                UserTokenDetails userTokenDetails = new UserTokenDetails();
                userTokenDetails.AgencyID = this.GetTenantID();
                userTokenDetails.UserID = this.GetUserID();
                var response = this.assessmentService.GetLastAssessmentByPerson(reportInputDTO.PersonIndex, reportInputDTO.PersonQuestionnaireID, reportInputDTO.PersonCollaborationID, reportInputDTO.VoiceTypeID, reportInputDTO.VoiceTypeFKID, userTokenDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetLastAssessmentByPerson/Post : GetLastAssessmentByPerson : Exception  : Exception occurred getting last assessment by person. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting last assessment by person. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentById.
        /// </summary>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponseDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment/{assessmentId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentResponsesDetailDTO> GetAssessmentById(int assessmentId)
        {
            try
            {
                var response = this.assessmentService.GetAssessmentById(assessmentId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetAssessmentById/GET : Getting item : Exception  : Exception occurred while getting assessment. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting assessment. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentByPersonQuestionaireIdAndStatus.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <param name="status">status.</param>
        /// <returns>AssessmentDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment-foralert/{personQuestionnaireId}/{status}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentResponsesDetailDTO> GetAssessmentByPersonQuestionaireIdAndStatus(long personQuestionnaireId, string status)
        {
            try
            {
                var response = this.assessmentService.GetAssessmentByPersonQuestionaireIdAndStatus(personQuestionnaireId, status);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetAssessmentByPersonQuestionaireIdAndStatus/GET : GET item : Exception  : Exception occurred while GetAssessmentByPersonQuestionaireIdAndStatus. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAssessmentByPersonQuestionaireIdAndStatus. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponsesDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment-response/{assessmentId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentResponsesDetailsDTO> GetAssessmentResponses(int assessmentId)
        {
            try
            {
                var response = this.assessmentService.GetAssessmentResponses(assessmentId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetAssessmentResponses/GET : GET item : Exception  : Exception occurred while GetAssessmentResponses. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAssessmentResponses. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// BatchUploadAssessments.
        /// </summary>
        /// <param name="assessmentDataToUpload">assessmentDataToUpload.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("import-assessments")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> BatchUploadAssessments([FromBody] UploadAssessmentDTO assessmentDataToUpload)
        {
            try
            {
                var response = this.assessmentService.BatchUploadAssessments(assessmentDataToUpload);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"BatchUploadAssessments/Post : POST item : Exception  : Exception occurred while uploading assessment. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Uploading Assessments. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetDetailsByPersonQuestionScheduleList.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>AssessmentResponsesDetailDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment-by-personquestionnireId/{personQuestionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentResponsesDetailDTO> GetAssessmentsByPersonQuestionaireID(long personQuestionnaireId)
        {
            try
            {
                var response = this.assessmentService.GetAssessmentsByPersonQuestionaireID(personQuestionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentsByPersonQuestionaireID/Get : Get item : Exception  : Exception occurred while GetAssessmentsByPersonQuestionaireID. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAssessmentsByPersonQuestionaireID. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentResponseFOrDashboardCalculation.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>AssessmentResponsesDetailsDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment-response-metrics-calculation/{personId}/{assessmentId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentResponsesDetailsDTO> GetAssessmentResponseFOrDashboardCalculation(long personId, int assessmentId)
        {
            try
            {
                var response = this.assessmentService.GetAssessmentResponseFOrDashboardCalculation(personId, assessmentId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentResponseFOrDashboardCalculation/Get : Get item : Exception  : Exception occurred while GetAssessmentResponseFOrDashboardCalculation. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetAssessmentResponseFOrDashboardCalculation. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>SkipLogicResponseDetailsDTO.</returns>
        [HttpGet]
        [Route("questionnaire-skip-logic/{questionnaireId}")]
        public ActionResult<SkipLogicResponseDetailsDTO> GetQuestionnaireSkipLogic(int questionnaireId)
        {
            try
            {
                var response = this.assessmentService.GetQuestionnaireSkipLogic(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireSkipLogic/Get : Get item : Exception  : Exception occurred while GetQuestionnaireSkipLogic. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetQuestionnaireSkipLogic. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetQuestionnaireSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>SkipLogicResponseDetailsDTO.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("questionnaire-skip-logic-anonymous/{questionnaireId}")]
        public ActionResult<SkipLogicResponseDetailsDTO> GetQuestionnaireSkipLogicAnonymous(int questionnaireId)
        {
            try
            {
                var response = this.assessmentService.GetQuestionnaireSkipLogic(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetQuestionnaireSkipLogic/Get : Get item : Exception  : Exception occurred while GetQuestionnaireSkipLogic. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetQuestionnaireSkipLogic. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentDetailsForEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDsList">ehrAssessmentIDsList.</param>
        /// <returns>EHRAssessmentResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("ehr-assessments-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EHRAssessmentResponseDTO> GetAssessmentDetailsForEHRUpdate(List<string> ehrAssessmentIDsList)
        {
            try
            {
                var ehrAssessmentIDs = string.Join(",", ehrAssessmentIDsList.ToArray());
                var response = this.assessmentService.GetAssessmentDetailsForEHRUpdate(ehrAssessmentIDs);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetAssessmentDetailsForEHRUpdate/GET : Getting item : Exception  : Exception occurred while getting details for EHRUpdate. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting details for EHRUpdate. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentDetailsForEHRUpdate.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>EHRAssessmentResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("ehr-assessments/{agencyID}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EHRAssessmentResponseDTO> GetAssessmentIDsForEHRUpdate(long agencyID)
        {
            try
            {
                var response = this.assessmentService.GetAssessmentIDsForEHRUpdate(agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetAssessmentIDsForEHRUpdate/GET : Getting item : Exception  : Exception occurred while getting Assessment IDs for EHRUpdate. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting Assessment IDs for EHRUpdate. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateAssessmentFlagAfterEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDsList">ehrAssessmentIDsList.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("ehr-assessments-updatestatus")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateAssessmentFlagAfterEHRUpdate(List<int> ehrAssessmentIDsList)
        {
            try
            {
                var response = this.assessmentService.UpdateAssessmentFlagAfterEHRUpdate(ehrAssessmentIDsList);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"UpdateAssessmentFlagAfterEHRUpdate/GET : Getting item : Exception  : Exception occurred while updating Assessment IDs for EHRUpdate. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating Assessment IDs for EHRUpdate. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetReceiversDetailsForReminderInvite.
        /// </summary>
        /// <param name="personQuestionnaireIds">personQuestionnaireIds.</param>
        /// <param name="typeOfReciever">typeOfReciever.</param>
        /// <returns>InviteMailReceiversDetailsResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("invitetocomplete-receivers-details/{typeOfReciever}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<InviteMailReceiversDetailsResponseDTO> GetReceiversDetailsForReminderInvite([FromBody] List<long> personQuestionnaireIds, string typeOfReciever)
        {
            try
            {
                var response = this.assessmentService.GetReceiversDetailsForReminderInvite(personQuestionnaireIds, typeOfReciever);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetReceiversDetailsForReminderInvite/POST : Adding item : Exception  : Exception occurred while GetReceiversDetailsForReminderInvite. {ex.Message}");
                return this.HandleException(ex, "An error occurred while GetReceiversDetailsForReminderInvite. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// BulkAddAssessmentsForReminders.
        /// </summary>
        /// <param name="assessmentDataToUpload">AssessmentBulkAddOnInviteDTO.</param>
        /// <returns>BulkAddAssessmentResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("reminderinvite-bulkupload-assessments")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<BulkAddAssessmentResponseDTO> BulkAddAssessmentsForReminders([FromBody] AssessmentBulkAddOnInviteDTO assessmentDataToUpload)
        {
            try
            {
                var response = this.assessmentService.BulkAddAssessmentsForReminders(assessmentDataToUpload);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"BulkAddAssessmentsForReminders/Post : POST item : Exception  : Exception occurred while BulkAddAssessmentsForReminders. {ex.Message}");
                return this.HandleException(ex, "An error occurred while BulkAddAssessmentsForReminders. Please try again later or contact support.");
            }
        }
    }
}