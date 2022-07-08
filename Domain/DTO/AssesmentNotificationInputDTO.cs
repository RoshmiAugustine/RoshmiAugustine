using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssesmentNotificationInputDTO
    {
        public int AssessmentID { get; set; }
        public string AssesmentNotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public int UpdateUserId { get; set; }
        public int? SubmittedUserID { get; set; }
    }
}
