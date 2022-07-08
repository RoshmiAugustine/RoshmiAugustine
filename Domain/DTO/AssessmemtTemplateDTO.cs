using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmemtTemplateDTO
    {
        public string CategoryFocus { get; set; }
        public string QuestionnaireItemName { get; set; }
        public string CategoryAndItemName
        {
            get
            {
                if (CategoryFocus == PCISEnum.ImportConstants.CategoryFocusSelf)
                    return QuestionnaireItemName;
                else
                    return string.Format("{0}_{1}", PCISEnum.ImportConstants.Caregiver, QuestionnaireItemName);
            }
        }
        public string DefaultItemvalue { get; set; }
        public int QuestionnaireItemID { get; set; }
    }
}
