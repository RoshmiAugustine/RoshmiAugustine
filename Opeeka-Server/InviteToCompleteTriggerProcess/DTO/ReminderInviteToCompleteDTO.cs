using System;
using System.Collections.Generic;
using System.Text;

namespace InviteToCompleteTriggerProcess.DTO
{
    public class RemindersToTriggerInviteToCompleteDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public RemindersToTriggerInviteToCompleteResponseDTO result { get; set; }
    }
    public class RemindersToTriggerInviteToCompleteResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<RemindersToTriggerInviteToCompleteDTO> ReminderInviteDetails { get; set; }
    }
    public class RemindersToTriggerInviteToCompleteDTO
    {
        public int QuestionnaireID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long PersonQuestionnaireScheduleID { get; set; }
        public int QuestionnaireWindowID { get; set; }
        public int AssessmentReasonID { get; set; }
        public string NotifyReminders { get; set; }
        public string InviteToCompleteReceivers { get; set; }
        public int NotifyReminderId { get; set; }
        public DateTime NotifyReminderDate { get; set; }
        public List<InviteToCompleteReceiversDTO> InviteToCompleteReceiversList { get; set; }
    }
    public class InviteToCompleteReceiversDTO
    {
        public int InviteToCompleteReceiverID { get; set; }
        public string Name { get; set; }
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
