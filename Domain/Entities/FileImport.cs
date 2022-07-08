using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class FileImport : BaseEntity
    {
        public int FileImportID { get; set; }
        public int ImportTypeID { get; set; }
        public string FileJsonData { get; set; }
        public int UpdateUserID { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedDate { get; set; }
        public long AgencyID { get; set; }
        public int? QuestionnaireID { get; set; }
        public string ImportFileName { get; set; }

        public Agency Agency { get; set; }
        public User UpdateUser { get; set; }
        public ImportType ImportType { get; set; }
        public Questionnaire Questionnaire { get; set; }

    }
}
