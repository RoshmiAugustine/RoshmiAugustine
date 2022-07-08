using System;
using System.Collections.Generic;
using System.Text;

namespace EHRProcess.Enums
{
    public class PCISEnum
    {
        public static class APIurl
        {
            public static string ConfigurationWithKey = "api/configurationvalue/{key}/{agencyID}";
            public static string ConfigurationForAgency = "api/configurations/{agencyID}";
            public static string LookupsForAgency = "api/lookups/{agencyID}";
            public static string PCISUpload = "api/person-upsert/{isclosed}";
            public static string GetEmailDetails = "api/emaildetails";
            public static string GetDetailsForEHRUpdate = "api/ehr-assessments-details";
            public static string GetAssessmentIDsForEHRUpdate = "api/ehr-assessments/{agencyID}";
            public static string UpdateAssessmentFlagAfterEHRUpdate = "api/ehr-assessments-updatestatus";
        }
        public static class APIMethodType
        {
            public const string GetRequest = "GET";
            public const string PostRequest = "POST";
        }

        public static class ConfigurationKey
        {
            public static string EHRAgencyIds = "EHR_AgencyIDs";
            public static string EHRLoginURL = "EHR_LoginURL";
            public static string EHRUsername = "EHR_Username";
            public static string EHRPassword = "EHR_Password";
            public static string EHRClientURL = "EHR_ClientURL";
            public static string EHRRelationshipURL = "EHR_RelationshipURL";
            public static string EHRProviderURL = "EHR_ProviderURL";
            public static string EHRDefaultCollaborationID = "EHRDefaultCollaborationID";
            public static string EHRHouseMemberURL = "EHR_HouseMemberURL";
            public static string EHRHomeURL = "EHR_HomeURL";
            public static string EHRAdoptionURL = "EHR_AdoptionURL";
            public static string EHRInstrumentsToUpdate = "EHR_InstrumentsToUpdate";
            public static string EHRTimezone = "EHR_TimeZone";
        }

        public static class APIReplacableValues
        {
            public static string Key = "{key}";
            public static string AgencyID = "{agencyID}";
            public static string StatusInURL = "{status}";
            public static string Isclosed = "{isclosed}";
        }
        public static class EnvironmentVariables
        {
            public static string ApiUrlFromKeyVault = "ApiUrlFromKeyVault";
            public static string ApiSecretFromKeyVault = "ApiSecretFromKeyVault";
            public static string EmailKeyFromKeyVault = "EmailKeyFromKeyVault";
        }

        public static class Constants
        {
            public static string UsernameForUrl = "&username=";
            public static string PasswordForUrl = "&Password=";
            public static string TokenName = "LtpaToken";
            public static string EHRRelationAPIFirstname = ".";
            public static string EHRPhoneCode = "+1";
            public static int EHRUpdateUserID = 1;
            public static string EHRProviderAPIRelation = "Foster Caregiver";
            public static string Bearer = "bearer";
            public static int UploadBlockCount = 20;
            public static int Hours = 48;
            public static int InsertionSuccess = 1001;
            public static string EHRDummyPhoneNumber = "5555555555";
            public static string EHRDummyDOB = "1/1/2000";
            public static string EHRDummyRace = "UnKnown";
            public static string OpenStatusInURL = "O";
            public static string CloseStatusInURL = "C";
            public static string CANS = "CANS";
            public static string RedirectURLAction = "&action=";
            public static string RedirectURLParam2 = "&PARM2=";
            public static string RedirectURLReplaceAction = "&ACTION=complete";
            public static int EHRDetailsFetchCount = 10;
        }

        public static class EHRClientAPIFields
        {
            public const string cc_firstname = "cc_firstname";
            public const string cc_lastname = "cc_lastname";
            public const string cc_middleinitial = "cc_middleinitial";
            public const string cc_dob = "cc_dob";
            public const string cc_gender = "cc_gender";
            public const string cc_personalphone = "cc_personalphone";
            public const string cc_personalemail = "cc_personalemail";
            public const string cc_race = "cc_race";
            public const string cc_primarylanguage = "cc_primarylanguage";
            public const string cc_therapist_unid = "cc_therapist_unid";
            public const string cc_referral_date = "cc_referral_date";
            public const string cc_relationship_unids = "cc_relationship_unids";
            public const string cc_home_unid = "cc_home_unid";
            public const string cc_close = "cc_close";
        }
        public static class EHRRelationshipAPIFields
        {
            public const string r_relationship_detail = "r_relationship_detail";
            public const string r_fullname = "r_fullname";
            public const string r_email = "r_email";
            public const string r_phone_work = "r_phone_work";
        }
        public static class EHRProviderAPIFields
        {
            public const string fh_name_last_a = "fh_name_last_a";
            public const string fh_name_first_a = "fh_name_first_a";
            public const string fh_name_last_b = "fh_name_last_b";
            public const string fh_name_first_b = "fh_name_first_b";
            public const string fh_name_middle_a = "fh_name_middle_a";
            public const string fh_name_middle_b = "fh_name_middle_b";
            public const string fh_email = "fh_email";
            public const string fh_phone_home = "fh_phone_home";
            public const string fh_relationship_detail = "fh_relationship_detail";
        }
        public static class EHRProviderAPIFieldsForHomeAPI
        {
            public const string fh_fullname = "fh_fullname";
            public const string fh_email2 = "fh_email2";
            public const string fh_member_unids = "fh_member_unids";
            public const string fh_secworker_unid = "fh_secworker_unid";
            public const string fh_intakedate = "fh_intakedate";
            public const string fh_worker_unid = "fh_worker_unid";
            public const string fh_dob_a = "fh_dob_a";
            public const string fh_providertype = "fh_providertype";
            public const string fh_race_a = "fh_race_a";
            public const string fh_race_b = "fh_race_b";
            public const string fh_close = "fh_closedate";
            public const string fh_gender_a = "fh_gender_a";
            public const string fh_gender_b = "fh_gender_b";

        }
        public static class EHRHouseMemberAPIFields
        {
            public const string hm_relationship_detail = "hm_relationship_detail";
            public const string hm_lastname = "hm_lastname";
            public const string hm_firstname = "hm_firstname";
            public const string hm_phone_home = "hm_phone_home";
            public const string hm_email = "hm_email";
            public const string hm_middleinitial = "hm_middleinitial";
        }
        public static class Lookups
        {
            public static string Gender = "Gender";
            public static string Race = "RaceEthinicity";
            public static string Language = "Language";
            public static string SupportType = "SupportType";
            public static string Helper = "ExternalHelper";
            public static string PCISPeopleCount = "UniveralsIDCount";
        }
        public static class FieldsToValidate
        {
            public const string Phone = "cc_gender";
            public const string Email = "cc_race";
            public const string Name = "cc_primarylanguage";
            public const string MiddleName = "cc_therapist_unid";
            public const string DOB = "DOB";
        }
        public static List<string> EHRProviderType = new List<string>() {
            "Level 1 - Basic Care".ToUpper(),
            "Level 1 - Fost/Adopt".ToUpper(),
            "Respite - Basic Care".ToUpper(),
            "Foster Family (Applicant)".ToUpper()
        };
    }
}

