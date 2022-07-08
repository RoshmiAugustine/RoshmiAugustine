using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{

    public class ReminderInviteToComplete : BaseEntity
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
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Status { get; set; }
        public int? UpdateUserId { get; set; }

        public Helper Helper { get; set; }
        public Person Person { get; set; }
        public Assessment Assessment { get; set; }
        public PersonSupport PersonSupport { get; set; }
    }
}
