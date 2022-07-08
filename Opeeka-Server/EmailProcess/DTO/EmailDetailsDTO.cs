using System;
using System.Collections.Generic;
using System.Text;

namespace EmailProcess.DTO
{
    public class EmailDetailsDTO
    {
        public long EmailDetailID { get; set; }
        public long? PersonID { get; set; }
        public int? HelperID { get; set; }
        public long? AgencyID { get; set; }
        public string Email { get; set; }
        public string EmailAttributes { get; set; }
        public string Status { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEmailReminderAlerts { get; set; }
        public int? FKeyValue { get; set; }
    }

    public class ReminderInviteToCompleteDTO
    {
        public long ReminderInviteToCompleteID { get; set; }
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