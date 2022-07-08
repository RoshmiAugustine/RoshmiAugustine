// -----------------------------------------------------------------------
// <copyright file="PCISEnum.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Infrastructure.Enums
{
    public class PCISEnum
    {
        public static class StatusCodes
        {
            public static int InsertionSuccess = 1001;
            public static int insertionFailed = 1002;
            public static int UpdationSuccess = 1003;
            public static int UpdationFailed = 1004;
            public static int DeletionSuccess = 1005;
            public static int DeletionFailed = 1006;
            public static int RequiredParameterMissing = 1007;
            public static int DeleteRecordInUse = 1008;
            public static int Success = 1010;
            public static int Failure = 1011;
            public static int InvalidLogin = 1012;
            public static int InvalidAbbrev = 1013;
            public static int InvalidName = 1013;
            public static int InvalidListOrder = 1013;
            public static int InvalidPassword = 1014;

            public static int SaveSuccess = 1013;
            public static int SaveFailed = 1014;
            public static int SubmitSuccess = 1015;
            public static int SubmitFailed = 1016;
            public static int EmailSendSuccess = 1017;
            public static int EmailSendFailed = 1018;
            public static int CloneSuccess = 1019;
            public static int UserExists = 1100;

            public static int NotificationSuccess = 1020;
            public static int NotificationFailed = 1021;
            public static int MissingEmailID = 1022;
            public static int InvalidEmailAssessmentURL = 1023;
            public static int InvalidVoiceType = 1024;
            public static int InvalidQuestionnaire = 1025;
            public static int InvalidExpired = 1026;
            public static int EmailAssessmentURLExpired = 1027;
            public static int EmailAssessmentAlreadySubmitted = 1028;
            public static int OtpExpired = 1029;
            public static int EmailAssessmentDeleted = 1030;
            public static int ImageUploadSuccess = 1032;
            public static int ImageUploadFailed = 1033;
            public static int AssessmentDeleted = 1031;
            public static int ErrorLog = 1034;
            public static int HTMLCodeDetected = 500;
            public static int ResetPasswordSuccess = 1034;
            public static int ResetPasswordFailed = 1035;
            public static int AssessmentExist = 1036;


            public static int APIRateLimitReached = 1037;
            public static int FileImportFailedOnHeaders = 1038;
            public static int FileImportFailedOnEmpty = 1039;
            public static int FileImportFailedOnDuplicateHeaders = 1040;
            public static int missingEmailAndTextPermission = 1041;
            public static int missingEmailPermission = 1042;
            public static int missingTextPermission = 1043;
            public static int TextSendSuccess = 1044;
            public static int TextAndEmailSendSuccess = 1045;
            public static int TextSendFailed = 1046;

        }

        public static class StatusMessages
        {
            public static string InsertionSuccess = "Successfully added.";
            public static string insertionFailed = "Insertion failed.";
            public static string UpdationSuccess = "Successfully updated.";
            public static string UpdationFailed = "Updation failed.";
            public static string DeletionSuccess = "Successfully deleted.";
            public static string DeletionFailed = "Deletion failed.";
            public static string RequiredParameterMissing = "Please enter a valid {0}.";
            public static string DeleteRecordInUse = "Cannot delete. Record already in use.";
            public static string Success = "Success.";
            public static string InvalidLogin = "Invalid Username/Password/Tenant";
            public static string CloneSuccess = "Newly cloned questionnaire is at ID ";

            public static string InvalidPassword = "Invalid Password";

            public static string Failure = "Failure.";
            public static string ValidationFailure = "Table is already in use.";
            public static string PermissionDenied = "Permission denied for user.";
            public static string InvalidAbbrev = "This Agency Abbreviation already exists. Please enter another Abbreviation.";
            public static string InvalidName = "This Agency Name already exists.Please enter another Name.";
            public static string InvalidListOrder = "List Order is already in use.";
            public static string SaveSuccess = "Successfully saved.";
            public static string SaveFailed = "Save failed.";
            public static string SubmitSuccess = "Successfully submitted.";
            public static string SubmitFailed = "Submission failed.";

            public static string SaveSuccessNotificationFailed = "Successfully saved and notification deletion failed";

            public static string EmailSendSuccess = "The mail has been sent successfully.";
            public static string TextSendSuccess = "The Text has been sent successfully.";
            public static string TextAndEmailSendSuccess = "The Text and Email has been sent successfully.";
            public static string EmailSendFailed = "Failed to send mail.";
            public static string UserAlreadyExists = "Email Already Exists";

            public static string NotificationSuccess = "Successfully saved Notification";
            public static string NotificationFailed = "Failed to save Notification.";

            public static string MissingEmailID = "EmailID is missing";
            public static string InvalidEmailAssessmentURL = "Invalid URL";
            public static string InvalidVoiceType = "Invalid voiceType({0})";
            public static string InvalidQuestionnaire = "Invalid questionnaire.";
            public static string EmailAssessmentURLExpired = "URL expired";
            public static string EmailOtpExpired = "Otp is expired";
            public static string InvalidEmailOtp = "Invalid Otp";
            public static string EmailAssessmentAlreadySubmitted = "Email assessment already submitted";
            public static string EmailAssessmentDeleted = "Email assessment deleted";
            public static string ImageUploadSuccess = "Image uploaded successfully.";
            public static string ImageUploadFailed = "Image upload failed.";
            public static string AssessmentDeleted = "Assessment deleted";
            public static string ErrorLog = "Error logged successfully.";
            public static string HTMLCodeDetected = "Invalid user input. HTML code detected.";
            public static string ResetPasswordFailed = "Failed to resend invitation.";
            public static string ResetPasswordSuccess = "Successfully resend invitation.";
            public static string AssessmentExist = "Cannot Delete. The questionnaire already has an assessment.";
            public static string APIRateLimitReached = "OTP requests exceeded! Allows only a maximum of {0} OTPs per minute. Please retry after {1}sec.";
            public static string InvalidHelperExternalID = "Helper external id already exists.Please enter another external id.";

            public static string AssessmentImportSuccess = "Successfully Uploaded Assessments.{0}.";
            public static string AssessmentImportFailed = "Failed To Upload Assessments.{0}";
            public static string FileImportFailedOnHeaders = "Imported file column headers do not match with default template.";
            public static string FileImportFailedOnDuplicateHeaders = "Imported file has column headers duplicated.";
            public static string FileImportFailedOnEmpty = "File is empty.";
            public static string missingEmailAndTextPermission = "Don't have permission to send Email or Text";
            public static string missingEmailPermission = "Don't have permission to send Email";
            public static string missingTextPermission = "Don't have permission to send Text";
            public static string InvalidHelperIndex = "Invalid HelperIndex";
            public static string LeadHelperMissing = "Please enter at least one active Lead Helper.";
            public static string PrimaryCollaborationMissing = "Please enter at least one active Primary Collboration.";
            public static string LeadHelperError = "More than one Lead Helper is not allowed.";
            public static string PrimaryCollaborationError = "More than one Primary collboration is not allowed.";
            public static string InvalidHelperId = "Please enter a valid PersonHelpers[{0}].HelperId.";
            public static string InvalidCollaborationId = "Please enter a valid PersonCollaborations[{0}].CollaborationId.";
            public static string InvalidCountryIdForStatetId = "Please enter a valid {0} for the entered StateId.";
            public static string RequireValidationInArrays = "Please enter a valid {0}[{1}].{2}.";
            public static string InvalidPrimaryKeysInEdit = "Invalid {0} used in edit: {1}.";
            public static string PowerBIValidation = "Invalid {0}"; 
            public static string PowerBIFailure = "Failed to retrieve report url details.";
        }

        public static class Constants
        {
            public static string Add = "Add";
            public static string Edit = "Edit";
            public static string Save = "Save";
            public static string Submit = "Submit";
        }

        public static class Parameters
        {
            public static string Name = "Name";
            public static string UserId = "User Id";
            public static string QuestionnaireID = "Questionnaire Id";
            public static string CountryStateId = "Country State Id";
            public static string PageNumber = "Page Number";
            public static string PageSize = "Page Size";
            public static string AgencyIndex = "Agency Index";
            public static string PeopleIndex = "People Index";
            public static string HelperIndex = "Helper Index";
            public static string tenantId = "tenantId";
            public static string QuestionnaireIndex = "Questionnaire Index";
            public static string ReportingUnitID = "ReportingUnit ID";
            public static string AgencyID = "Agency ID";
            public static string VoiceTypeID = "Voice Type ID";
            public static string CollaborationIndex = "Collaboration Index";
            public static string AssessmentID = "Assessment ID";
            public static string PersonQuestionnaireID = "Person Questionnaire ID";
            public static string PersonCollaborationID = "Person Collaboration ID";
            public static string VoiceTypeFKID = "Voice Type FKID";
            public static string personIndex = "personIndex";
            public static string IsSharedPermission = "IsSharedPermission";
            public static string CollaborationSharingIndex = "CollaborationSharingIndex";
            public static string AgencySharingIndex = "AgencySharingIndex";
            public static string IsActivePerson = "IsActivePerson";
            public static string PersonId = "Person ID";
            public static string CountryID = "Country ID";
            public static string CountryCode1 = "Phone1Code";
            public static string CountryCode2 = "Phone2Code";
            public static string Email = "Email";
            public static string IdentificationGenderID = "Identification Gender ID";
            public static string GenderID = "Gender ID";
            public static string SexualityID = "Sexuality ID";
            public static string PrimaryLanguageID = "PrimaryLanguage ID";
            public static string PreferredLanguageID = "PreferredLanguage ID";
            public static string PersonIdentificationType = "PersonIdentificationType";
            public static string IdentificationNumber = "IdentificationNumber";
            public static string RaceEthnicityID = "RaceEthnicityID";
            public static string CollaborationID = "CollaborationID";
            public static string HelperID = "HelperID";
            public static string IsLead = "IsLead";
            public static string Phone1 = "Phone1";
            public static string Phone2 = "Phone2";
            public static string HelperTitleID = "HelperTitleID";
            public static string RoleId = "RoleId";
            public static string SupervisorHelperID = "SupervisorHelperID";
            public static string ReviewerID = "ReviewerID";
            public static string DateOfBirth = "DateOfBirth";
            public static string SupportTypeID = "SupportTypeID";
            public static string Phone = "Phone";
            public static string CountryCode = "Country Code";
            public static string Zip = "Zip";
            public static string TherapyTypeID = "TherapyTypeID";
            public static string CollaborationLevelID = "CollaborationLevelID";
            public static string PersonHelperID = "PersonHelperID";
            public static string PersonCollaborationId = "PersonCollaborationID";
            public static string EndDate = "End Date";
            public static string PersonSupportID = "PersonSupportID";

        }
        public static class Roles
        {
            public static string SuperAdmin = "Super Admin";
            public static string OrgAdmin = "OrgAdmin";
            public static string OrgAdminRO = "Organization Admin RO";
            public static string OrgAdminRW = "Organization Admin RW";
            public static string Helper = "Helper";
            public static string HelperRW = "Helper RW";
            public static string HelperRO = "Helper RO";
            public static string Supervisor = "Supervisor";
            public static string Support = "Support";
            public static string Person = "Person";
            public static string APIUser = "API User";
            public static string Assessor = "Assessor";
        }
        public static class PermissionsClaim
        {
            public static string APIEndPointPermission = "APIEndPointPermission";
            public static string ModulePermission = "ModulePermission";
            public static string NamespacePermission = "NamespacePermission";
        }
        public static class ApplicationObjectTypes
        {
            public static string APIEndPoint = "APIEndPoint";
            public static string Module = "Module";
            public static string Namespace = "Namespace";
            public static string UIComponentButton = "UIComponentButton";
            public static string UIComponentMenu = "UIComponentMenu";


        }
        public static class SharedPermissionsClaim
        {
            public static string APIEndPointSharedPermission = "APIEndPointSharedPermission";
            public static string ModuleSharedPermission = "ModuleSharedPermission";
            public static string NamespaceSharedPermission = "NamespaceSharedPermission";
        }

        public static class ConfigurationParameters
        {
            public static string Culture = "Culture";
            public static string DateTimeFormat = "DateTimeFormat";
            public static string Domain = "Domain";
            public static string PCIS_BaseURL = "PCIS_BaseURL";
            public static string PeopleQuestionnaireURL = "PeopleQuestionnaireURL";
            public static string LanguageLocalization = "Language_Localization";
            public static string AssessmentAutoSave = "Assessment_AutoSave";
            public static string Reminder_LimitInMonth_If_EndDate_Null = "Reminder_LimitInMonth_If_EndDate_Null";
            public static string ApplicationTimeout = "Application_Timeout";
            public static string AssesmentPageSize = "Assesment_PageSize";
            public static string AssessmentFileTypes = "Assessment_FileTypes";
            public static string PowerBIWorkspaceId = "PowerBI_WorkspaceId"; 
            public static string SSOConfig = "SSO_RedirectURL";
            public static string SSOConfigReplace = "{{redirecturl}}";
        }
        public static class TokenClaims
        {
            public static string UserId = "UserId";
            public static string Roles = "Roles";
            public static string TenantId = "TenantId";
            public static string AgencyAbbrev = "AgencyAbbrev";
            public static string Culture = "Culture";
            public static string DateTimeFormat = "DateTimeFormat";
            public static string extension_AssociatedTenents = "extension_AssociatedTenents";
            public static string Email = "Email";
            public static string RoleID = "RoleID";
            public static string EHRRequest = "EHRRequest";
            public static string InstanceURL = "InstanceURL";

        }
        public static class TokenHeaders
        {
            public static string tenantUrl = "tenantUrl";
            public static string timeZone = "timeZone";
            public static string Culture = "Culture";
            public static string agencyId = "agencyId";
        }

        public static class AssessmentStatus
        {
            public static string InProgress = "In Progress";
            public static string Submitted = "Submitted";
            public static string Complete = "Complete";
            public static string Approved = "Approved";
            public static string EmailSent = "Email Sent";
            public static string Returned = "Returned";
        }

        public static class NotificationStatus
        {
            public static string Unresolved = "Unresolved";
            public static string Resolved = "Resolved";
        }

        public static class ComparisonOperator
        {
            public const string Equal = "=";
            public const string EqualEqual = "==";
            public const string NotEqual = "!=";
            public const string GreaterThan = ">";
            public const string LessThan = "<";
            public const string GreaterThanOrEqual = ">=";
            public const string LessThanOrEqual = "<=";
        }

        public static class NotificationType
        {
            public static string Alert = "Alert";
            public static string Reminder = "Reminder";
        }
        public static class AssessmentNotificationType
        {
            public static string Submit = "AssessmentSubmit";
            public static string EmailSubmit = "EmailAssessmentSubmit";
            public static string Approve = "AssessmentApproved";
            public static string Reject = "AssessmentRejected";
        }
        public static class AssessmentReason
        {
            public static string Trigger = "Triggering Event";
            public static string Initial = "Initial";
            public static string Scheduled = "Scheduled";
            public static string Discharge = "Discharge";
        }

        public static class Caching
        {
            public static string APIPermissions = "Permn";
            public static string GetAllState = "GetAllState";
            public static string GetAllCountries = "GetAllCountries";
            public static string GetAllRolesLookup = "GetAllRolesLookup";
            public static string GetAllHelperLookup = "GetAllHelperLookup";
            public static string GetAllItemResponseBehavior = "GetAllItemResponseBehavior";
            public static string GetAllCollaboration = "GetAllCollaboration";
            public static string GetAllNotificationLevel = "GetAllNotificationLevel";
            public static string GetAllQuestionnaireItems = "GetAllQuestionnaireItems";
            public static string GetAllQuestionnaire = "GetAllQuestionnaire";
            public static string GetAllActionType = "GetAllActionType";
            public static string GetAllCategories = "GetAllCategories";
            public static string GetAllLeads = "GetAllLeads";
            public static string GetAllLevels = "GetAllCollaborationLevels";
            public static string GetAllTherapyTypes = "GetAllTherapyTypes";
            public static string GetAllResponse = "GetAllResponse";
            public static string GetAllAgencyLeads = "GetAllAgencyLeads";
            public static string GetCollaborationLevelLookup = "GetCollaborationLevelLookup";
            public static string GetAgencyTherapyTypeList = "GetAgencyTherapyTypeList";
            public static string GetAgencyNotificationLevelList = "GetAgencyNotificationLevelList";
            public static string GetAgencyTagTypeList = "GetAgencyTagTypeList";
            public static string GetApplicationObjectTypeList = "ApplicationObjectTypes";
            public static string GetAllSystemRoles = "GetAllSystemRoles";
            public static string GetAllItemResponseType = "GetAllItemResponseType";
            public static string GetAllAssessmentStatus = "GetAllAssessmentStatus";
            public static string GetAllNotificationType = "GetAllNotificationTypes";
            public static string GetAllVoiceType = "GetAllVoiceTypes";
            public static string GetAllSupportType = "GetAllSupportTypes";

            public static string GetAllTimeZones = "GetAllTimeZones";
            public static string GetAllOffsetType = "GetAllOffsetType";
            public static string GetAllRecurrenceEndType = "GetAllRecurrenceEndType";
            public static string GetAllRecurrencePattern = "GetAllRecurrencePattern";
            public static string GetAllRecurrenceOrdinal = "GetAllRecurrenceOrdinal";
            public static string GetAllRecurrenceDay = "GetAllRecurrenceDay";
            public static string GetAllRecurrenceMonth = "GetAllRecurrenceMonth";
            public static string GetAllInviteToComplete = "GetAllInviteToComplete";
            public static string GetAllResponseValueType = "GetAllResponseValueType";
            public static string GetAllAssessmentReason = "GetAllAssessmentReason";
            public static string GetAllNotificationStatus = "GetAllNotificationStatus";
        }
        public static class ItemResponseType
        {
            public static string Exposure = "Exposure";
            public static string Need = "Need";
            public static string Strength = "Strength";
            public static string SupportResource = "Support Resource";
            public static string SupportNeed = "Support Need";
            public static string SupportExposure = "Support Exposure";
            public static string Unmodifiable = "Unmodifiable";
            public static string SupportUnmodifiable = "Support Unmodifiable";
            public static string Circumstance = "Circumstance";
            public static string Preference = "Preference";
            public static string Goal = "Goal";
            public static string Opinion = "Opinion";
        }
        public static class ToDo
        {
            public static string Underlying = "Underlying";
            public static string Background = "Background";
            public static string Focus = "Focus";
            public static string Build = "Build";
            public static string Use = "Use";
            public static string None = "None";
        }
        public static class VoiceType
        {
            public static string Communimetric = "Communimetric";
            public static string Consumer = "Consumer";
            public static string Support = "Support";
            public static string Helper = "Helper";
        }
        public static class AssessmentEmail
        {
            public static string ConfigKeyToURL = "EmailAssessmentURL";
            public static string FromEmail = "FromEmailID";
            public static string FromDisplayName = "FromEmailDisplayName";
            public static string EmailText = "AssessmentEmailText";
            public static string EmailSubject = "AssessmentEmailSubject";
            public static string EmailLinkExpiry = "AssessmentEmailLinkExpiry";
            public static string emailUrlCodeReplaceText = "{{emailurl}}";
            public static string emailexpiryCodeReplaceText = "{{expiry}}";
            public static string applicationUrlReplaceText = "{{applicationUrl}}";
            public static string personNameReplaceText = "{{personname}}";
            public static string AssessmentNotes = "{{assessmentnotes}}";
            public static string OtpCallExpiry = "OtpCallExpiry";
        }
        public static class B2cEmail
        {
            public static string FromEmail = "FromEmailID";
            public static string FromDisplayName = "FromEmailDisplayName";
            public static string EmailText = "SignupEmailText";
            public static string EmailSubject = "SignupEmailSubject";
            public static string applicationUrlReplaceText = "{{applicationUrl}}";
            public static string personNameReplaceText = "{{personname}}";
            public static string emailUrlCodeReplaceText = "{{signupurl}}";
            public static string emailPasswordReplaceText = "{{temporarypassword}}";
        }

        public static class B2cCustomAttributes
        {
            public const string B2cAgency = "agency";
            public const string B2cRole = "role";
            public const string B2cTenantId = "tenantId";
            public const string B2cUserId = "userId";
            public const string B2cTenantAbbreviation = "tenantAbbreviation";
            public const string B2cDateTimeFormat = "dateTimeFormat";
            public const string B2cAccess = "access";
            public const string B2cMustResetPassword = "mustResetPassword";
            public const string B2cInstanceURL = "instanceURL";
        }

        public static class B2cClaims
        {
            public const string B2cEmail = "email";
            public const string B2cName = "name";
            public const string B2cAgency = "extension_agency";
            public const string B2cRole = "extension_role";
            public const string B2cTenantId = "extension_tenantId";
            public const string B2cUserId = "extension_userId";
            public const string B2cTenantAbbreviation = "extension_tenantAbbreviation";
            public const string B2cDateTimeFormat = "extension_dateTimeFormat";
            public const string B2cRPOCTFP = "tfp";
            public const string B2cAuthenticationSource = "authenticationSource";
            public const string B2cInstanceURL = "extension_instanceURL";
        }

        public static class Options
        {
            public static string CollaborationLevel = "CollaborationLevel";
            public static string CollaborationTagType = "CollaborationTagType";
            public static string TherapyType = "TherapyType";
            public static string HelperTitle = "HelperTitle";
            public static string NotificationLevel = "NotificationLevel";
            public static string Gender = "Gender";
            public static string IdentificationType = "IdentificationType";
            public static string Sexuality = "Sexuality";
            public static string RaceEthnicity = "RaceEthnicity";
            public static string SupportType = "SupportType";
            public static string IdentifiedGender = "IdentifiedGender";
            public static string Language = "Language";
        }

        public static class AssessmentEmailOTP
        {
            public static string otpChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            public static string otpLength = "AssessmentEmailOTPLength";
            public static string otpTimeSpan = "AssessmentEmailOTPExpiry";
            public static string ConfigKeyToURL = "EmailAssessmentURL";
            public static string EmailText = "AssessmentOTPEmailText";
            public static string EmailSubject = "AssessmentOTPEmailSubject";
            public static string OTPCodeReplaceText = "{{otpcode}}";
            public static string expiryCodeReplaceText = "{{expiry}}";
            public static string applicationUrlReplaceText = "{{applicationUrl}}";
        }
        public static class QuestionnaireReminderType
        {
            public const string WindowOpen = "Window Open";
            public const string AssesmentDue = "Assesment Due";
            public const string WindowClose = "Window Close";
            public const string QuestionnaireLate = "Questionnaire Late";
            public const string QuestionnaireOverdue = "Questionnaire Overdue";
        }

        public static class AzureConstants
        {
            public static string Container = "temporary";
            public static string ContainerFolder = "profileImages";
            public static string SSOAuthentication = "socialIdpAuthentication";
            public static string SuperAdminFolder = "SuperAdmin";
        }

        public static class ReminderNotification
        {
            public static string Reminder_LimitInMonth_If_EndDate_Null = "Reminder_LimitInMonth_If_EndDate_Null";
            public static string Reminder_Count_After_WindowCloseDay = "Reminder_Count_After_WindowCloseDay";
        }

        public static class SharingPolicies
        {
            public static string ReadOnly = "ReadOnly";
            public static string ReadWrite = "Read/Write";
        }

        public static class Limits
        {
            public static int StatusLimit = 100;
            public static int StatusCount = 0;
        }
        public static class APIRateLimit
        {
            public static string Count = "APIRateLimitCount";
            public static string Period = "APIRateLimitPeriod";
            public static string Endpoint = "APIRateLimitEndPoint";
        }

        public static class EHRLookups
        {
            public static string Gender = "Gender";
            public static string Race = "RaceEthinicity";
            public static string Language = "Language";
            public static string SupportType = "SupportType";
            public static string Helper = "ExternalHelper";
            public static string UniveralsIDCount = "UniveralsIDCount";
        }
        public static class EmailDetail
        {
            public static string AssessmentID = "{assessmentid}";
            public static string PersonIndex = "{personindex}";
            public static string QuestionnaireId = "{questionnaireid}";
            public static string NotificationType = "{notificationtype}";
            public const string ReminderOverDue = "reminderoverdue";
            public const string ReminderDueDate = "reminderduedate";
            public const string ReminderDueApproching = "reminderdueapproching";
            public const string InviteToComplete = "invitetocomplete";
        }
        public static class Notes
        {
            public static string Reason = "ReasonNotes";
            public static string Added = "AddedNotes";
            public static string Returned = "ReturnedNotes";
            public static string Approved = "ApprovedNotes";
            public static string Trigger = "TriggerEventNotes";
        }
        public static class CallingType
        {
            public static string Dashboard = "Dashboard";
            public static string ViewAll = "ViewAll";
            public static string EHR = "EHR";
            public static string ExternalAPI = "ExternalAPI";
        }
        public static class ActiveFilter
        {
            public static string Active = "Active";
            public static string Inactive = "Inactive";
            public static string All = "All";
        }

        public static class ExportReplace
        {
            public static string agencyID = "{agencyId}";
            public static string assessmentFilter = "{assessmentFilterConditions}";
        }

        public static class ImportTypes
        {
            public static string Person = "Person";
            public static string Helper = "Helper";
            public static string Assessment = "Assessment";
        }
        public static class ExportTypes
        {
            public static string Person = "Person";
            public static string Helper = "Helper";
            public static string Assessment = "Assessment";
        }
        public static class ImportConstants
        {
            public static string CategoryFocusSelf = "Self";
            public static string Caregiver = "PersonSupport1";
        }
        public static class ImportAssessmentLookups
        {
            public static string AssessmentReasons = "AssessmentReasons";
            public static string VoiceTypes = "VoiceTypes";
            public static string AssessmentStatus = "AssessmentStatus";
        }
        public static class ImportEmail
        {
            public static string EmailTemplateText = "ImportEmailText";
            public static string EmailSubject = "ImportEmailSubject";
            public static string ResultMessage = "{{importresultmessage}}";
            public static string HelperName = "{{helpername}}";
            public static string Importfilename = "{{importfilename}}";
        }

        public static class AuditPersonProfileType
        {
            public static string Helper = "Helper";
            public static string Collaboration = "Collaboration";
        }
        public static class EHRUpdateStatus
        {
            public static string Pending = "Pending";
            public static string Done = "Done";
        }
        public static class B2cRPOC
        {
            public static string Username = "username";
            public static string Password = "password";
            public static string GrantType = "grant_type";
            public static string Scope = "scope";
            public static string ClientId = "client_id";
            public static string Openid = "openid";
            public static string Response_type = "response_type";
            public static string Token = "token";

        }
        public static class AssessmentSMS
        {

            public static string ConfigKeyToBody = "AssessmentSMSBody";
            public static string ConfigKeyToFromNo = "AssessmentSMSFromNumber";
            public static string StopKey = "AssessmentSMSStop";
            public static string StopMessage = "AssessmentSMSUnsubscribe";
            public static string emailUrlReplaceCode = "{{assessmenturl}}";
            public static int SMSOtp = -34566;
        }
        public static class Invitation
        {
            public static string SMS = "SMS";
            public static string Email = "Email";
        }
        public static class ResponseValueType
        {
            public static string TextArea = "TextArea";
            public static string Date = "Date";
            public static string Checkbox = "Checkbox";
            public static string Doodle = "Doodle";
            public static string Signature = "Signature";
            public static string Attachment = "Attachment";
            public static string ExcludedValueTypes = "'TextArea','Date','Checkbox','Doodle','Signature','Attachment'";
        }
        public static class AzureQueues
        {
            //Queue name should always be in lowercase.
            public static string Dashboardmetricscalculation = "dashboardmetricscalculation";
            public static string Assessmentremindernotification = "assessmentremindernotification";
            public static string Assessmentrisknotification = "assessmentrisknotification";
            public static string Resolvenotificationalerts = "resolvenotificationalerts";
            public static string Resolveremindernotifications = "resolveremindernotifications";
        }
        public static class InputDTOValidationPattern
        {
            //For Firstname,lastname,middlename for Person
            public const string Name = @"^[^`~@#$%\^&*+=|\d]{0,150}$";
            //For ColborationName for Collaboration
            public const string CollabName = "^[^`~!@#$%\\^&*()+={}|[\\]\\\\:';\"\"<>?,./]{0,150}$";
            //For ColborationCode for Collaboration
            public const string CollabCode = "^[^`~!@#$%\\^&*()+={}|[\\]\\\\:';\"\"<>?,./]{0,50}$";
            //For ColborationAbbrev for Collaboration
            public const string CollabAbbrev = "^[a-zA-Z0-9\\s()/_-]{0,20}$";
            //For Firstname,lastname,middlename for Helper
            public const string HelperName = "^[^`~!@#$%\\^&*+={}|[\\]\\\\:;\"\"<>?,/\\d]{0,150}$";
            //Include only digits
            public const string PhoneNumber_US = @"^[\d]{10}$";
            public const string PhoneNumber = @"^{0,50}$";
            public const string PhoneCode = @"^[+][\d]+$";
            public const string Zip_US = @"^[\d]{5}$";
            public const string Zip = @"^[a-zA-z\d]{8}$";
            public const string Email = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        }
        public static class InputDTOValidationMessages
        {
            public const string ValidateName = "Please enter a valid Name.";
            public const string ValidateFirstName = "Please enter a valid First Name.";
            public const string ValidateLastName = "Please enter a valid Last Name.";
            public const string ValidateMiddleName = "Please enter a valid Middle Name.";
            public const string ValidatePhoneCode = "Please enter a valid PhoneCode.";
            public const string ValidatePhone1Code = "Please enter a valid Phone1Code.";
        }
        public static class Insights
        {
            public const string key = "Insights";
            public const string Filters = "Insights_Filters";
            public const string SisenseAPIURL = "Insights_SisenseAPIURL";
            public const string SisenseURLDataPath = "Insights_SisenseURLDataPath";
            public const string LifeInSeconds = "Insights_LifeInSeconds";
            public const string SisenseDashboardID = "Insights_SisenseDashboardID";
            public const string SisenseAPISecretKey = "Insights_SisenseAPISecretKey";
            public const string CustomFilters = "Insights_CustomFilters";
            public const string CustomFilterAgencyId = "{{AgencyId}}";
            public const string CustomFilterUserRole = "{{UserRole}}";
            public const string CustomFilterUserId = "{{UserId}}";
        }

        public static class RecurrencePatternGroup
        {
            public const string Daily = "Daily";
            public const string Weekly = "Weekly";
            public const string Monthly = "Monthly";
            public const string Yearly = "Yearly";
        }
        public static class RecurrencePattern
        {
            public const string DailyDays = "DailyDays";
            public const string DailyWeekdays = "DailyWeekdays";
            public const string Weekly = "Weekly";
            public const string MonthlyByDay = "MonthlyByDay";
            public const string MonthlyByOrdinalDay = "MonthlyByOrdinalDay";
            public const string YearlyByMonth = "YearlyByMonth";
            public const string YearlyByOrdinal = "YearlyByOrdinal";
        }
        public static class RecurrenceOrdinal
        {
            public const string First = "First";
            public const string Second = "Second";
            public const string Third = "Third";
            public const string Fourth = "Fourth";
            public const string Last = "Last";
        }
        public static class OffsetType
        {
            public const char Day = 'd';
            public const char Hour = 'h';
        }
        public static class RecurrenceEndType
        {
            public const string EndByEndate = "EndByEndDate";
            public const string EndByNumberOfOccurences = "EndByNmbrOfOccurence";
            public const string EndByNoEndate = "EndByNoEndDate";
        }
        public static class EmailStatus
        {
            public static string Pending = "Pending";
            public static string Sent = "Sent";
            public static string Failed = "Failed";
            public static string Processing = "Processing";
        }
        public static class InviteToCompleteReceivers
        {
            public static string Helpers = "All Helpers";
            public static string LeadHelper = "Lead Helper";
            public static string Person = "Person In Care";
            public static string Supports = "Supports";
        }

        public static class AssessmentImageResponses
        {
            public static string StorageAccountPattern = @"AccountName=([a-zA-Z0-9]+)\;";
            public static string Container = "assessment-image-responses";
            public static string AgencyFolder = "Agency-{0}";
            public static string AssessmentFolder = "Assessment-{0}";
            #region Doodle/Signature
            public const string FileExtension = "png";
            //0 - AssessmententResponseGuid , 2 - ImageFileExtension
            public static string FileName = $"{{0}}.{FileExtension}";
            public static string ContentType = $"image/{FileExtension}";
            #endregion

            #region Attachments
            //0 - AssessmentResponseId, 1 - FileResponseGuid , 2 - FileExtension
            public static string AttachmentFileName = "{0}-{1}.{2}";
            //0-FileExtension names from DB config pipe separated (without dot extension for file types) 
            //ex:^.*\.(jpg|JPG|gif|GIF|doc|DOC|pdf|PDF)$
            public static string FileTypeExtensionPattern = @"^.*\.({0})$";
            #endregion
            //0 -StorageAccount, 1-AssessmentImageContainer, 2-AgencyFolder, 3-AssessmentFolder, 4 - AssessmentResponseIdFileNAme
            public static string ImageURL = "https://{0}.blob.core.windows.net/{1}/{2}/{3}/{4}";
            public static string doodleParentContainer = "doodle-background-images";
        }

        public static class APIMethodType
        {
            public const string GetRequest = "GET";
            public const string PostRequest = "POST";
        }
        public static class PowerBIFilterReplace
        {
            public const string VoiceTypeName = "{{VoiceTypeName}}";
            public const string CollaborationName = "{{CollaborationName}}";
            public const string QuestionnaireName = "{{QuestionnaireName}}";
            public const string QuestionnaireId = "{{QuestionnaireId}}";
            public const string CollaborationId = "{{CollaborationId}}";
            public const string PersonId = "{{PersonId}}";
            public const string PersonIndex = "{{PersonIndex}}";
            public const string AgencyId = "{{AgencyId}}";
            public const string UserId = "{{UserId}}";
            //0-ParameterColumnName 1-ColumnValue
            public const string Parameter = "&rp:{0}={1}";
        }
    }
}
