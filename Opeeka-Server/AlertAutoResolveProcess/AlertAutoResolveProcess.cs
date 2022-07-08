using System;
using System.Collections.Generic;
using System.Linq;
using AlertAutoResolveProcess.Common;
using AlertAutoResolveProcess.DTO;
using AlertAutoResolveProcess.DTOt;
using AlertAutoResolveProcess.Enums;
using AlertAutoResolveProcess.Output;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlertAutoResolveProcess
{
    public static class AlertAutoResolveProcess
    {
        [FunctionName("AlertAutoResolveProcess")]
        public static void Run([QueueTrigger("resolvenotificationalerts", Connection = "QueueStorageUrlFromKeyVault")]string myQueueItem, ILogger log)
        {
            log.LogInformation($" AlertAutoResolveProcess : C# Queue trigger function processed: {myQueueItem}");
            try
            {
                Utility Utility = new Utility();
                var assessmentId = Convert.ToInt32(myQueueItem);
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem}");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                var assessment = GetAssessmentByID(Utility, apiurl, assessmentId, log, myQueueItem);

                if (assessment != null)
                {
                    //get persion id
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID Start");
                    var personQuestionnaire = GetPersonQuestionnaireByID(Utility, apiurl, assessment.PersonQuestionnaireID, log, myQueueItem);
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID end");

                    //get removed notification log details
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetNotificationLogForNotificationResolveAlert Start");
                    var removedNotificationLogs = GetNotificationLogForNotificationResolveAlert(Utility, apiurl, personQuestionnaire.PersonID, log, myQueueItem);
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetNotificationLogForNotificationResolveAlert end");

                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetNotificationStatus Start");
                    int notificationResolutionStatusID = GetNotificationStatus(Utility, apiurl, PCISEnum.NotificationStatus.Resolved).NotificationResolutionStatusID;
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetNotificationStatus end");

                    if (removedNotificationLogs.Count > 0)
                    {
                        foreach (var notificationLog in removedNotificationLogs)
                        {
                            notificationLog.NotificationResolutionStatusID = notificationResolutionStatusID;
                            notificationLog.UpdateDate = DateTime.UtcNow;
                        }
                        //update removed log status
                        UpdateBulkNotificationLog(Utility, apiurl, removedNotificationLogs);
                    }

                    //take unresolved notification logs
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetUnResolvedNotificationLogForNotificationResolveAlert Start");
                    var unresolvedNotificationLogs = GetUnResolvedNotificationLogForNotificationResolveAlert(Utility, apiurl, personQuestionnaire.PersonID, log, myQueueItem);
                    log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetUnResolvedNotificationLogForNotificationResolveAlert end");


                    if (unresolvedNotificationLogs.Count > 0)
                    {
                        log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetAssessmentResponse Start");
                        var assessmentResponses = GetAssessmentResponse(Utility, apiurl, assessment.AssessmentID, log,myQueueItem);
                        log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetAssessmentResponse end");

                        log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : HandleRiskNotificationAlerts Start");
                        List<NotificationLogDTO> notificationLogs = HandleRiskNotificationAlerts(Utility, apiurl, assessmentResponses, unresolvedNotificationLogs, notificationResolutionStatusID);
                        log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : HandleRiskNotificationAlerts end");

                        if (notificationLogs.Count > 0)
                        {
                            log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : UpdateBulkNotificationLog Start");
                            UpdateBulkNotificationLog(Utility, apiurl, notificationLogs);
                            log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : UpdateBulkNotificationLog end");

                        }
                    }

                }
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : End");

            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"AlertAutoResolveProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static CRUDResponse UpdateBulkNotificationLog(Utility Utility, string apiurl, List<NotificationLogDTO> notificationLogs)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.UpdateNotificationLog;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, notificationLogs.ToJSON());
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

        private static List<RiskNotificationsListDTO> GetUnResolvedNotificationLogForNotificationResolveAlert(Utility Utility, string apiurl, long personId, ILogger log, string myQueueItem)
        {
            try
            {
                List<RiskNotificationsListDTO> notificationLog = new List<RiskNotificationsListDTO>();
                var url = apiurl + PCISEnum.APIurl.UnresolvedNotificationLogByPersonId.Replace(PCISEnum.APIReplacableValues.PersonId, Convert.ToString(personId));
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetUnResolvedNotificationLogForNotificationResolveAlert url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var notificationLogResponse = JsonConvert.DeserializeObject<RiskNotificationResponseDTO>(result);
                    notificationLog = notificationLogResponse?.result?.NotificationLog;
                }
                return notificationLog;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<NotificationLogDTO> GetNotificationLogForNotificationResolveAlert(Utility Utility, string apiurl, long personId, ILogger log, string myQueueItem)
        {
            try
            {
                List<NotificationLogDTO> notificationLog = new List<NotificationLogDTO>();
                var url = apiurl + PCISEnum.APIurl.NotificationLogByPersonId.Replace(PCISEnum.APIReplacableValues.PersonId, Convert.ToString(personId));
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetNotificationLogForNotificationResolveAlert url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var notificationLogResponse = JsonConvert.DeserializeObject<NotificationLogResponseDTO>(result);
                    notificationLog = notificationLogResponse?.result?.NotificationLog;
                }
                return notificationLog;
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
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
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

        private static PersonQuestionnaireDTO GetPersonQuestionnaireByID(Utility Utility, string apiurl, long personQuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                PersonQuestionnaireDTO PersonQuestionnaire = new PersonQuestionnaireDTO();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireByID.Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(personQuestionnaireId));
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
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

        private static AssessmentDTO GetAssessmentByID(Utility Utility, string apiurl, int assessmentId, ILogger log, string myQueueItem)
        {
            try
            {
                AssessmentDTO Assessment = new AssessmentDTO();
                var url = apiurl + PCISEnum.APIurl.GetAssessmentById.Replace(PCISEnum.APIReplacableValues.AssessmentId, Convert.ToString(assessmentId));
                log.LogInformation($" AlertAutoResolveProcess : Queue Item : {myQueueItem} : GetAssessmentByID url : {url}");
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

        /// <summary>
        /// Handle Risk Notification Alerts
        /// </summary>
        /// <param name="assessmentResponses"></param>
        /// <param name="unresolvedNotificationLogs"></param>
        /// <returns>List<NotificationLog></returns>
        public static List<NotificationLogDTO> HandleRiskNotificationAlerts(Utility Utility, string apiurl, IReadOnlyList<AssessmentResponsesDTO> assessmentResponses, List<RiskNotificationsListDTO> unresolvedNotificationLogs, int resolvedStatusID)
        {
            List<NotificationLogDTO> notificationLogs = new List<NotificationLogDTO>();

            foreach (var unresolvedNotifyRiskRule in unresolvedNotificationLogs)
            {
                if (unresolvedNotifyRiskRule?.QuestionnaireNotifyRiskRuleID == 0)
                {
                    continue;
                }
                var questionnaireNotifyRiskRuleConditionList = GetQuestionnaireNotifyRiskRuleConditionByRuleID(Utility,apiurl, unresolvedNotifyRiskRule.QuestionnaireNotifyRiskRuleID);

                bool? notify = null;
                foreach (var item in questionnaireNotifyRiskRuleConditionList)
                {
                    var response = assessmentResponses.Where(x => x.QuestionnaireItemID == item.QuestionnaireItemId).FirstOrDefault();
                    if (response !=null)
                    {
                        var responseDto = GetResponse(Utility, apiurl, response.ResponseID);
                        switch (item?.ComparisonOperator)
                        {
                            case PCISEnum.ComparisonOperator.Equal:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value == item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value == item?.ComparisonValue;
                                }
                                break;
                            case PCISEnum.ComparisonOperator.EqualEqual:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value == item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value == item?.ComparisonValue;
                                }
                                break;
                            case PCISEnum.ComparisonOperator.GreaterThan:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value > item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value > item?.ComparisonValue;
                                }
                                break;
                            case PCISEnum.ComparisonOperator.GreaterThanOrEqual:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value >= item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value >= item?.ComparisonValue;
                                }
                                break;
                            case PCISEnum.ComparisonOperator.LessThan:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value < item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value < item?.ComparisonValue;
                                }
                                break;
                            case PCISEnum.ComparisonOperator.LessThanOrEqual:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value <= item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value <= item?.ComparisonValue;
                                }
                                break;
                            case PCISEnum.ComparisonOperator.NotEqual:
                                if (item?.JoiningOperator == null || (item?.JoiningOperator != null ? item?.JoiningOperator.ToLower() == "and" : true))
                                {
                                    notify = (notify ?? true) && responseDto?.Value != item?.ComparisonValue;
                                }
                                else
                                {
                                    notify = (notify ?? false) || responseDto?.Value != item?.ComparisonValue;
                                }
                                break;
                            default:
                                notify = null;
                                break;
                        }
                    }

                }
                if (notify == !true)
                {
                    var notificationLog = new NotificationLogDTO
                    {
                        NotificationLogID = unresolvedNotifyRiskRule.NotificationLogID,
                        NotificationDate = unresolvedNotifyRiskRule.NotificationDate,
                        PersonID = unresolvedNotifyRiskRule.PersonID,
                        NotificationTypeID = unresolvedNotifyRiskRule.NotificationTypeID,
                        NotificationResolutionStatusID = resolvedStatusID,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = unresolvedNotifyRiskRule.UpdateUserID,
                        FKeyValue = unresolvedNotifyRiskRule.NotifyRiskID,
                        IsRemoved = unresolvedNotifyRiskRule.IsRemoved,
                        NotificationData = unresolvedNotifyRiskRule.NotificationData,
                        StatusDate = unresolvedNotifyRiskRule.StatusDate,
                        Details = unresolvedNotifyRiskRule.Details,
                        HelperName = unresolvedNotifyRiskRule.HelperName
                    };
                    notificationLogs.Add(notificationLog);
                }
            }
            return notificationLogs;
        }
    }
}
