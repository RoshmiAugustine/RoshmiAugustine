using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.Enums
{
    public class PCISEnum
    {
        public static class APIurl
        {
            public static string ConfigurationWithKey = "api/configurationvalue/{key}/{agencyID}";
            public static string ConfigurationForAgency = "api/configurations/{agencyID}";
            public static string LookupsForAgency = "api/import-assessment-lookups/{agencyID}/{questionnaireID}";
            public static string ImportFileList = "api/import-file-list/?importType={importTypeName}";
            public static string HelperList = "api/helper-list-byemail";
            public static string CollaberationList = "api/collaboration-list-byname";
            public static string EthinicityList = "api/race-ethnicity-byname";
            public static string IdentifierList = "api/identification-types-byname";
            public static string ImportPerson = "api/import-person";
            public static string importIsprocessedUpdate = "api/import-isprocessed-update";
            public static string getAllIdentifiedGender = "api/get-all-identified-gender";
            public static string rolesByname = "api/roles-byname";
            public static string getAllCountryCodes = "api/country-code";

            public static string AssessmentTemplate = "api/assessment-template-json-import/{questionnaireID}";
            public static string ImportAssessmentFileList = "api/import-file-list/?importType={importTypeName}";
            public static string PersonVoiceTypeDetails = "api/person-support-helper-details";
            public static string UploadAssessments = "api/import-assessments";
            public static string sendEmailImport = "api/send-email-import";

            public static string emailValidation = "api/email-validation";
            public static string importHelper = "api/import-helper";


        }
        public static class APIMethodType
        {
            public const string GetRequest = "GET";
            public const string PostRequest = "POST";
            public const string PutRequest = "PUT";
        }


        public static class APIReplacableValues
        {
            public static string Key = "{key}";
            public static string AgencyID = "{agencyID}";
            public static string ImportType = "{importTypeName}"; 
            public static string QuestionnaireID = "{questionnaireID}"; 
        }
        public static class EnvironmentVariables
        {
            public static string ApiUrlFromKeyVault = "ApiUrlFromKeyVault";
            public static string ApiSecretFromKeyVault = "ApiSecretFromKeyVault";
            public static string EmailKeyFromKeyVault = "EmailKeyFromKeyVault";
            public static string RootPath = "RootPath";
        }

        public static class Constants
        {
            public static int InsertionSuccess = 1001;
            public static int UploadBlockCount = 20;
            public static string TokenName = "LpaToken";
            public static string Bearer = "bearer";
            public static string Caregiver = @"PersonSupport";
            public static string CaregiverRegx1 = @"PersonSupport(\d)$";
            public static string CaregiverFormatWithNumber = "PersonSupport{0}_";
            public static string CaregiverAppendedOnItem = "PersonSupport1_";
            public static string CaregiverRegx2 = @"PersonSupport(\d)";
        }


        public static class PersonFields
        {
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string DateOfBirth = "DateOfBirth";
            public const string Email = "Email";
            public const string Phone1 = "Phone1";
            public const string Phone1Code = "Phone1Code";
            public const string IdentifiedGender = "Identified Gender";
            public const string HelperStartDate = "HelperStartDate";
            public const string CollaborationStartDate = "CollaborationStartDate";
            public const string HelperEmail = "HelperEmail";
            public const string CollaborationName = "CollaborationName";
            public const string Ethinicity = "Ethinicity";
            public const string RaceEthnicity1 = "Race/Ethnicity1";
            public const string RaceEthnicity2 = "Race/Ethnicity2";
            public const string RaceEthnicity3 = "Race/Ethnicity3";
            public const string RaceEthnicity4 = "Race/Ethnicity4";
            public const string RaceEthnicity5 = "Race/Ethnicity5";
            public const string IdentifierType = "IdentifierType";
            public const string IdentifierType1 = "IdentifierType1";
            public const string IdentifierType2 = "IdentifierType2";
            public const string IdentifierType3 = "IdentifierType3";
            public const string IdentifierType4 = "IdentifierType4";
            public const string IdentifierType5 = "IdentifierType5";
            public const string IdentifierTypeID1 = "IdentifierTypeID1";
            public const string IdentifierTypeID2 = "IdentifierTypeID2";
            public const string IdentifierTypeID3 = "IdentifierTypeID3";
            public const string IdentifierTypeID4 = "IdentifierTypeID4";
            public const string IdentifierTypeID5 = "IdentifierTypeID5";
        }

        public static class HelperFields
        {
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Role = "Role";
            public const string Email = "Email";
            public const string Agency = "Agency";
            public const string ReviewerEmail = "ReviewerEmail";
            public const string StartDate = "StartDate";
            public const string SendSignUpMail = "ReceiveSignUPMail(True/False OR 1/0)";
            public const string SendSignUpMailText = "ReceiveSignUpMail";
        }
       
        public static class ConfigurationKey
        {
            public static string AlertTemplate = "AlertTemplate";
        }
       public static class ImportTypes
        {
            public static string Person = "Person";
            public static string Helper = "Helper";
            public static string Assessment = "Assessment";
        }
        public class FileImportFields
        {
            public static string ImportType = "importType";
            public static string JsonData = "fileJsonData";
            public static string AgencyID = "agencyID";
            public static string UpdateUserID = "updateUserID";
            public static string FileImportID = "fileImportID";
            public static string QuestionnaireID = "questionnaireID";
            public static string ImportFileName = "importFileName";
        }
        public class AsessmentTemplateFixedFields
        {
            public static string PersonIndex = "PersonIndex";
            public static string AssessmentDateTaken = "AssessmentDateTaken";
            public static string ReasoningText = "ReasoningText";
            public static string AssessmentReason = "AssessmentReason";
            public static string TriggeringEventDate = "TriggeringEventDate";
            public static string TriggeringEventNotes = "TriggeringEventNotes";
            public static string VoiceType = "VoiceType";
            public static string PersonSupportID = "PersonSupportID";
            public static string HelperEmail = "HelperEmail";
            public static string AssessmentStatus = "AssessmentStatus";

        }
        public static class ImportAssessmentLookups
        {
            public static string PersonIndex = "PersonIndex";
            public static string AssessmentReasons = "AssessmentReasons";
            public static string VoiceTypes = "VoiceTypes";
            public static string Helpers = "Helpers";
            public static string AssessmentStatus = "AssessmentStatus";
            public static string QuestionnaireItems = "QuestionnaireItems";
            public static string QuestionnaireDetails = "QuestionnaireDetails";
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
        public static class AssessmentReason
        {
            public static string TriggeringEvent = "Triggering Event";
            public static string Initial = "Initial";
            public static string Scheduled = "Scheduled";
            public static string Discharge = "Discharge";
        }
        public static class VoiceType
        {
            public static string Communimetric = "Communimetric";
            public static string Consumer = "Consumer";
            public static string Support = "Support";
            public static string Helper = "Helper";
        }
    }
}

