using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class RemindersToTriggerInviteToCompleteDTO
    {
        public int QuestionnaireID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long PersonQuestionnaireScheduleID { get; set; }
        public int QuestionnaireWindowID { get; set; }
        public int AssessmentReasonID { get; set; }
        public int NotifyReminderId { get; set; }
        public DateTime NotifyReminderDate { get; set; }
        public string InviteToCompleteReceivers { get; set; }
    }
    public class InviteReceiversInDetailDTO
    {
        public long PersonQuestionnaireID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int VoiceTypeID { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public string EmailID { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public string Phone { get; set; }
        public string PhoneCode { get; set; }
        public string DisplayName { get; set; }
        public int AssessmentStatusID { get; set; }
        public int? HelperId { get; set; }
        public Guid PersonIndex { get; set; }
        public long PersonId { get; set; }
        public string InviteReceiverType { get; set; }
        public long AgencyID { get; set; }
    }

    public class ReminderInviteToCompleteDTO
    {
        public long ReminderInviteToCompleteID { get; set; }
        public int? QuestionnaireID { get; set; }
        public int NotifyReminderID { get; set; }
        public int AssessmentID { get; set; }
        public int InviteToCompleteReceiverID { get; set; }
        public long? PersonID { get; set; }
        public int? HelperID { get; set; }
        public int? PersonSupportID { get; set; }
        public string Attributes { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AssessmentURL { get; set; }
        public string TypeOfInviteSend { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public Guid? AssesmentEmailLinkGUID { get; set; }
        public Guid AssesmentEmailLinkIndex { get; set; }
    }
}
