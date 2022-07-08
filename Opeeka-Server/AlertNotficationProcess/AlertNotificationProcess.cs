using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlertNotficationProcess.Common;
using AlertNotficationProcess.DTO;
using AlertNotficationProcess.Enums;
using AlertNotficationProcess.Output;
using AlertNotificationProcess.DTO;
using AlertNotificationProcess.DTOt;
using AlertNotificationProcess.Output;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlertNotficationProcess
{
    public static class AlertNotificationProcess
    {
        [FunctionName("AlertNotificationProcess")]
        public static void Run([QueueTrigger("assessmentrisknotification", Connection = "QueueStorageUrlFromKeyVault")]string myQueueItem, ILogger log)
        {
            log.LogInformation($" AlertNotificationProcess : C# Queue trigger function processed: {myQueueItem}");
            try
            {
                Utility Utility = new Utility();
                var assessmentId = Convert.ToInt32(myQueueItem);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem}");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentByID Start");
                var assessment = GetAssessmentByID(Utility, apiurl, assessmentId, log, myQueueItem);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentByID ENd");

                string submittedUserName = string.Empty;
                if (assessment.SubmittedUserID.HasValue)
                {
                    var submittedHelperDTO = GetHelperByUserID(Utility, apiurl, assessment.SubmittedUserID.Value, log, myQueueItem);
                    submittedUserName = submittedHelperDTO.Name;
                }

                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentByPersonQuestionaireIdAndStatus Start");
                var latestSubmittedAssessment = GetAssessmentByPersonQuestionaireIdAndStatus(Utility, apiurl, assessment.PersonQuestionnaireID, PCISEnum.AssessmentStatus.Approved, log, myQueueItem);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentByPersonQuestionaireIdAndStatus End");

                int notificationResolutionStatusID = 0;
                if (latestSubmittedAssessment != null && latestSubmittedAssessment.AssessmentID == assessment.AssessmentID)
                {
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationStatus Start");
                    notificationResolutionStatusID = GetNotificationStatus(Utility, apiurl, PCISEnum.NotificationStatus.Unresolved).NotificationResolutionStatusID;
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationStatus end");

                }
                else
                {
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationStatus start");
                    notificationResolutionStatusID = GetNotificationStatus(Utility, apiurl, PCISEnum.NotificationStatus.Resolved).NotificationResolutionStatusID;
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationStatus end");
                }
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID start");
                var personQuestionnaire = GetPersonQuestionnaireByID(Utility, apiurl, assessment.PersonQuestionnaireID, log, myQueueItem);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID end");

                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentResponse start");
                var assessmentResponses = GetAssessmentResponse(Utility, apiurl, assessment.AssessmentID, log, myQueueItem);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentResponse end");

                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireNotifyRiskRuleBySchedule start");
                var questionnaireNotifyRiskRulesDTO = GetQuestionnaireNotifyRiskRuleBySchedule(Utility, apiurl, personQuestionnaire.QuestionnaireID, log, myQueueItem);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireNotifyRiskRuleBySchedule end");


                List<NotificationLogDTO> notificationLogList = new List<NotificationLogDTO>();
                List<EmailDetailsDTO> emailDetailList = new List<EmailDetailsDTO>();

                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : CheckQuestionaireAlertNotificationEnabled start");
                var questionnaireDTO = CheckQuestionaireAlertNotificationEnabled(Utility, apiurl, personQuestionnaire.QuestionnaireID);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : CheckQuestionaireAlertNotificationEnabled end");
                List<PeopleHelperEmailDTO> personHelpers = new List<PeopleHelperEmailDTO>();
                if (questionnaireDTO.IsAlertsHelpersManagers)
                {
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : getPersonsAndHelpersByPersonIDListForAlert start");
                    personHelpers = GetPersonsAndHelpersByPersonIDListForAlert(Utility, apiurl, personQuestionnaire.PersonID, log, myQueueItem);
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : getPersonsAndHelpersByPersonIDListForAlert end");
                }
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetConfigurationValue start");
                var PCISBaseURL = GetConfigurationValue(Utility, apiurl, PCISEnum.ConfigurationParameters.PCIS_BaseURL);
                var PeopleQuestionnaireURL = GetConfigurationValue(Utility, apiurl, PCISEnum.ConfigurationParameters.PeopleQuestionnaireURL);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetConfigurationValue end");


                foreach (var questionnaireNotifyRiskRule in questionnaireNotifyRiskRulesDTO)
                {
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationLevel start");
                    var notificationLevel = GetNotificationLevel(Utility, apiurl, questionnaireNotifyRiskRule.NotificationLevelID);
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationLevel end");

                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireNotifyRiskRuleConditionByRuleID start");
                    var questionnaireNotifyRiskRuleConditionList = GetQuestionnaireNotifyRiskRuleConditionByRuleID(Utility, apiurl, questionnaireNotifyRiskRule.QuestionnaireNotifyRiskRuleID);
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireNotifyRiskRuleConditionByRuleID end");

                    bool? notify = null;
                    List<NotifyRiskValueDTO> notifyRiskValueList = new List<NotifyRiskValueDTO>();
                    foreach (var item in questionnaireNotifyRiskRuleConditionList)
                    {
                        NotifyRiskValueDTO notifyRiskValue = new NotifyRiskValueDTO();
                        var assessmentResponse = assessmentResponses.Where(x => x.QuestionnaireItemID == item.QuestionnaireItemId).FirstOrDefault();
                        if (assessmentResponse != null)
                        {
                            var responseDto = GetResponse(Utility, apiurl, assessmentResponse.ResponseID);
                            switch (item.ComparisonOperator)
                            {
                                case PCISEnum.ComparisonOperator.Equal:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || (responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder);
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.EqualEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || (responseDto?.Value == item?.ComparisonValue && responseDto?.ListOrder == item?.ListOrder);
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.GreaterThan:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value > item.ComparisonValue ? true : responseDto?.ListOrder > item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value > item.ComparisonValue ? true : responseDto?.ListOrder > item?.ListOrder;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.GreaterThanOrEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value >= item.ComparisonValue ? true : responseDto?.ListOrder >= item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value >= item.ComparisonValue ? true : responseDto?.ListOrder >= item?.ListOrder;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.LessThan:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value < item.ComparisonValue ? true : responseDto?.ListOrder < item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value < item.ComparisonValue ? true : responseDto?.ListOrder < item?.ListOrder;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.LessThanOrEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value <= item.ComparisonValue ? true : responseDto?.ListOrder <= item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value <= item.ComparisonValue ? true : responseDto?.ListOrder <= item?.ListOrder;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.NotEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value != item.ComparisonValue ? true : responseDto?.ListOrder != item?.ListOrder;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value != item.ComparisonValue ? true : responseDto?.ListOrder != item?.ListOrder;
                                    }
                                    break;
                                default:
                                    notify = null;
                                    break;
                            }
                            if (notify == true)
                            {
                                notifyRiskValue = new NotifyRiskValueDTO()
                                {
                                    AssessmentResponseID = assessmentResponse.AssessmentResponseID,
                                    AssessmentResponseValue = responseDto.Value,
                                    QuestionnaireNotifyRiskRuleConditionID = item.QuestionnaireNotifyRiskRuleConditionID
                                };
                                notifyRiskValueList.Add(notifyRiskValue);
                            }
                        }
                    }
                    if (notify == true)
                    {
                        log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationType start");
                        var notificationType = GetNotificationType(Utility, apiurl, PCISEnum.NotificationType.Alert);
                        log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetNotificationType end");

                        AssessmentNotificationRiskDTO assessmentNotificationRisk = new AssessmentNotificationRiskDTO();
                        assessmentNotificationRisk.AssessmentID = assessment.AssessmentID;
                        assessmentNotificationRisk.PersonID = personQuestionnaire.PersonID;
                        assessmentNotificationRisk.UpdateUserID = 1;
                        assessmentNotificationRisk.NotificationResolutionStatusID = notificationResolutionStatusID;
                        assessmentNotificationRisk.QuestionnaireNotifyRiskRuleID = questionnaireNotifyRiskRule.QuestionnaireNotifyRiskRuleID;
                        assessmentNotificationRisk.NotificationTypeID = notificationType.NotificationTypeID;
                        assessmentNotificationRisk.NotifyRiskValueList = notifyRiskValueList;
                        assessmentNotificationRisk.QuestionnaireID = personQuestionnaire.QuestionnaireID;
                        assessmentNotificationRisk.Detail = personQuestionnaire.QuestionnaireID + " - " + questionnaireNotifyRiskRule.Name + " - " + notificationLevel.Name;
                        var notificationLog = HandleRiskNotification(Utility, apiurl, assessmentNotificationRisk, log, myQueueItem);
                        notificationLog.HelperName = submittedUserName;
                        notificationLogList.Add(notificationLog);

                        foreach (var item in personHelpers)
                        {
                            EmailDetailsDTO emailDetail = new EmailDetailsDTO
                            {
                                Email = item.HelperEmail,
                                AgencyID = item.AgencyID,
                                EmailAttributes = "PersonInitial = " + item.PersonInitials + " | DisplayName = " + item.HelperFirstName + " " + item.HelperMiddleName + " " + item.HelperLastName,
                                HelperID = item.HelperID,
                                PersonID = personQuestionnaire.PersonID,
                                Status = "Pending",
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = 1,
                                CreatedDate = DateTime.UtcNow,
                                Type = PCISEnum.NotificationType.Alert,
                                URL = PCISBaseURL + new StringBuilder(PeopleQuestionnaireURL).Replace("{assessmentid}", assessmentId.ToString()).Replace("{personindex}", item.PersonIndex.ToString()).Replace("{notificationtype}", PCISEnum.NotificationType.Alert).Replace("{questionnaireid}", personQuestionnaire.QuestionnaireID.ToString()).ToString(),
                                FKeyValue = notificationLog.FKeyValue
                            };
                            emailDetailList.Add(emailDetail);
                        }
                    }
                }
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddBulkNotificationLog start");
                AddBulkNotificationLog(Utility, apiurl, notificationLogList);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddBulkNotificationLog end");

                if (emailDetailList.Count > 0)
                {
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddEmailDetails start");
                    AddEmailDetails(Utility, apiurl, emailDetailList);
                    log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddEmailDetails end");
                }

            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"AlertNotificationProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static QuestionnaireDTO CheckQuestionaireAlertNotificationEnabled(Utility utility, string apiurl, int QuestionnaireID)
        {
            try
            {
                QuestionnaireDTO response = new QuestionnaireDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetQuestionaireDetails.Replace(PCISEnum.APIReplacableValues.QuestionnaireId, Convert.ToString(QuestionnaireID));
                var result = utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<QuestionnaireResponseDTO>(result);
                    response = responseDetails.result.Questionnaire;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse AddEmailDetails(Utility Utility, string apiurl, List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.EmailDetails;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, emailDetails.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse AddBulkNotificationLog(Utility Utility, string apiurl, List<NotificationLogDTO> notificationLog)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotificationLog;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notificationLog.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse AddNotifyRisk(Utility Utility, string apiurl, NotifyRiskDTO notifyRisk)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotifyRisk;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notifyRisk.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static NotificationLogDTO HandleRiskNotification(Utility Utility, string apiurl, AssessmentNotificationRiskDTO assessmentNotificationRiskDTO, ILogger log, string myQueueItem)
        {

            //NotifyRisk
            NotifyRiskDTO notifyRisk = new NotifyRiskDTO
            {
                QuestionnaireNotifyRiskRuleID = assessmentNotificationRiskDTO.QuestionnaireNotifyRiskRuleID,
                PersonID = assessmentNotificationRiskDTO.PersonID,
                AssessmentID = assessmentNotificationRiskDTO.AssessmentID,
                NotifyDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = assessmentNotificationRiskDTO.UpdateUserID
            };
            log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddNotifyRisk start");
            var response = AddNotifyRisk(Utility, apiurl, notifyRisk);
            log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddNotifyRisk end");

            //NotificationLog
            var notificationLog = new NotificationLogDTO
            {
                NotificationDate = DateTime.UtcNow,
                PersonID = assessmentNotificationRiskDTO.PersonID,
                NotificationTypeID = assessmentNotificationRiskDTO.NotificationTypeID,
                NotificationResolutionStatusID = assessmentNotificationRiskDTO.NotificationResolutionStatusID,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = assessmentNotificationRiskDTO.UpdateUserID,
                FKeyValue = response.Id,
                QuestionnaireID = assessmentNotificationRiskDTO.QuestionnaireID,
                AssessmentID = assessmentNotificationRiskDTO.AssessmentID,
                Details = assessmentNotificationRiskDTO.Detail,
                IsRemoved = false
            };
            if (assessmentNotificationRiskDTO.NotifyRiskValueList != null)
            {
                assessmentNotificationRiskDTO.NotifyRiskValueList.ForEach(x => x.NotifyRiskID = response.Id);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddBulkNotifyRiskValue start");
                AddBulkNotifyRiskValue(Utility, apiurl, assessmentNotificationRiskDTO.NotifyRiskValueList);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : AddBulkNotifyRiskValue end");

            }
            return notificationLog;
        }

        private static CRUDResponse AddBulkNotifyRiskValue(Utility Utility, string apiurl, List<NotifyRiskValueDTO> notifyRiskValues)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotifyRiskValues;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notifyRiskValues.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static NotificationTypeDTO GetNotificationType(Utility Utility, string apiurl, string type)
        {
            try
            {
                NotificationTypeDTO response = new NotificationTypeDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.NotificationTypeByName.Replace(PCISEnum.APIReplacableValues.notificationType, Convert.ToString(type));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<NotificationTypeListResponseDTO>(result);
                    response = responseDetails.result.NotificationType;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static NotificationResolutionStatusDTO GetNotificationStatus(Utility Utility, string apiurl, string status)
        {
            try
            {
                NotificationResolutionStatusDTO response = new NotificationResolutionStatusDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.NotificationStatus.Replace(PCISEnum.APIReplacableValues.Status, Convert.ToString(status));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<NotificationResolutionStatusDetailsDTO>(result);
                    response = responseDetails.result.NotificationStatus;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static ResponseDTO GetResponse(Utility Utility, string apiurl, int responseId)
        {
            try
            {
                ResponseDTO response = new ResponseDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetResponse.Replace(PCISEnum.APIReplacableValues.ResponseId, Convert.ToString(responseId));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<ResponseDetailsDTO>(result);
                    response = responseDetails.result.response;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static List<QuestionnaireNotifyRiskRuleConditionDTO> GetQuestionnaireNotifyRiskRuleConditionByRuleID(Utility Utility, string apiurl, int ruleId)
        {
            try
            {
                List<QuestionnaireNotifyRiskRuleConditionDTO> riskRuleCondition = new List<QuestionnaireNotifyRiskRuleConditionDTO>();
                var configAPiurl = apiurl + PCISEnum.APIurl.RiskRuleCondition.Replace(PCISEnum.APIReplacableValues.RuleId, Convert.ToString(ruleId));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var riskRuleConditionResponse = JsonConvert.DeserializeObject<QuestionnaireNotifyRiskRulesConditionDetailsDTO>(result);
                    riskRuleCondition = riskRuleConditionResponse.result.NotifyRiskRuleConditions;
                }
                return riskRuleCondition;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static NotificationLevelDTO GetNotificationLevel(Utility Utility, string apiurl, int notificationLevelID)
        {
            try
            {
                NotificationLevelDTO notificationLevel = new NotificationLevelDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetNotificationLevel.Replace(PCISEnum.APIReplacableValues.NotificationlevelId, Convert.ToString(notificationLevelID));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var notificationLevelResponse = JsonConvert.DeserializeObject<NotificationLevelResponseDTO>(result);
                    notificationLevel = notificationLevelResponse.result.NotificationLevel;
                }
                return notificationLevel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetConfigurationValue(Utility Utility, string apiurl, string configurationKey)
        {
            try
            {
                string configuration = string.Empty;
                var configAPiurl = apiurl + PCISEnum.APIurl.ConfigurationWithKey.Replace(PCISEnum.APIReplacableValues.Key, configurationKey).Replace(PCISEnum.APIReplacableValues.AgencyID, "0");
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var config = JsonConvert.DeserializeObject<ConfigurationResponseDTO>(result);
                    configuration = config.result.ConfigurationValue;
                }
                return configuration;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PeopleHelperEmailDTO> GetPersonsAndHelpersByPersonIDListForAlert(Utility Utility, string apiurl, long personId, ILogger log, string myQueueItem)
        {
            try
            {
                List<PeopleHelperEmailDTO> personHelper = new List<PeopleHelperEmailDTO>();
                var url = apiurl + PCISEnum.APIurl.PersonHelperByPersonId.Replace(PCISEnum.APIReplacableValues.PersonId, Convert.ToString(personId));
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var personHelperResponses = JsonConvert.DeserializeObject<PersonHelperEmailDetailDTO>(result);
                    personHelper = personHelperResponses?.result?.PersonHelper;
                }
                return personHelper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<QuestionnaireNotifyRiskRulesDTO> GetQuestionnaireNotifyRiskRuleBySchedule(Utility Utility, string apiurl, int questionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                List<QuestionnaireNotifyRiskRulesDTO> notifyRisk = new List<QuestionnaireNotifyRiskRulesDTO>();
                var url = apiurl + PCISEnum.APIurl.RiskRuleByQuestionnaireId.Replace(PCISEnum.APIReplacableValues.QuestionnaireId, Convert.ToString(questionnaireId));
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var notifyRiskResponses = JsonConvert.DeserializeObject<QuestionnaireNotifyRiskRulesDetailsDTO>(result);
                    notifyRisk = notifyRiskResponses?.result?.NotifyRiskRules;
                }
                return notifyRisk;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<AssessmentResponsesDTO> GetAssessmentResponse(Utility Utility, string apiurl, int assessmentId, ILogger log, string myQueueItem)
        {
            try
            {
                List<AssessmentResponsesDTO> assessmentResponse = new List<AssessmentResponsesDTO>();
                var url = apiurl + PCISEnum.APIurl.AssessmentResponseByAssessmentID.Replace(PCISEnum.APIReplacableValues.AssessmentId, Convert.ToString(assessmentId));
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var assessmentResponses = JsonConvert.DeserializeObject<AssessmentResponsesDetailsDTO>(result);
                    assessmentResponse = assessmentResponses?.result?.AssessmentResponses;
                }
                return assessmentResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static PersonQuestionnaireDTO GetPersonQuestionnaireByID(Utility Utility, string apiurl, long personQuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                PersonQuestionnaireDTO PersonQuestionnaire = new PersonQuestionnaireDTO();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireByID.Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(personQuestionnaireId));
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireResponse = JsonConvert.DeserializeObject<PersonQuestionnaireDetailsDTo>(result);
                    PersonQuestionnaire = PersonQuestionnaireResponse?.result?.PersonQuestionnaire;
                }
                return PersonQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static AssessmentDTO GetAssessmentByPersonQuestionaireIdAndStatus(Utility Utility, string apiurl, long personQuestionnaireID, string status, ILogger log, string myQueueItem)
        {
            try
            {
                AssessmentDTO Assessment = new AssessmentDTO();
                var url = apiurl + PCISEnum.APIurl.GetAssessmentByPersonQuestionaireIdAndStatus
                    .Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(personQuestionnaireID)).Replace(PCISEnum.APIReplacableValues.Status, status);
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var AssessmentResponse = JsonConvert.DeserializeObject<AssessmentResponseDetailsDTO>(result);
                    Assessment = AssessmentResponse?.result?.Assessment;
                }
                return Assessment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static AssessmentDTO GetAssessmentByID(Utility Utility, string apiurl, int assessmentId, ILogger log, string myQueueItem)
        {
            try
            {
                AssessmentDTO Assessment = new AssessmentDTO();
                var url = apiurl + PCISEnum.APIurl.GetAssessmentById.Replace(PCISEnum.APIReplacableValues.AssessmentId, Convert.ToString(assessmentId));
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetAssessmentByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var AssessmentResponse = JsonConvert.DeserializeObject<AssessmentResponseDetailsDTO>(result);
                    Assessment = AssessmentResponse?.result?.Assessment;
                }
                return Assessment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static UserDetailsDTO GetHelperByUserID(Utility Utility, string baseapiurl, long userId, ILogger log, string myQueueItem)
        {
            try
            {
                UserDetailsDTO userDetails = new UserDetailsDTO();
                var url = baseapiurl + PCISEnum.APIurl.GetHelperDetailsForAssessment.Replace(PCISEnum.APIReplacableValues.userId, Convert.ToString(userId));
                log.LogInformation($" AlertNotificationProcess : Queue Item : {myQueueItem} : GetHelperDetailsForAssessment url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponseDTO>(result);
                    userDetails = userDetailsResponse?.result?.UserDetails;
                }
                return userDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
