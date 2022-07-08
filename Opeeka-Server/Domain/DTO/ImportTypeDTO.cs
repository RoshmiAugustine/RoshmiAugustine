using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class ImportTypeDTO
    {
        public int ImportTypeID { get; set; }
        public string Name { get; set; }
        public string TemplateJson { get; set; }
        public string TemplateURL { get; set; }
        public List<QuestionnaireDTO> Questionnaires { get; set; }
    }
}
