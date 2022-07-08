using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentResponseAttachment : BaseEntity
    {
        public int AssessmentResponseAttachmentID { get; set; }
        public int AssessmentResponseID { get; set; }
        public string FileURL { get; set; }
        public string FileName { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? AddedByVoiceTypeID { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public Guid AssessmentResponseFileGUID { get; set; }

        public User UpdateUser { get; set; }
        public AssessmentResponse AssessmentResponse { get; set; }


    }
}
