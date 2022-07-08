using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperPersonCollaborationDTO
    {
        public int CollaborationID { get; set; }
        public int QuestionnaireID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long PersonID { get; set; }
        public int NotificationLogID { get; set; }
    }
    public class HelperPersonCollaborationDetailsDTO
    {
        public List<int> CollaborationIDs { get; set; }
        public List<int> QuestionnaireIDs { get; set; }
        public List<long> PersonQuestionnaireIDs { get; set; }
        public List<long> PersonIDs { get; set; }
        public List<int> NotificationLogIDs { get; set; }
    }
}
