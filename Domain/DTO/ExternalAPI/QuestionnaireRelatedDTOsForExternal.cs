using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class QuestionnaireSearchInputDTO : Paginate
    {
        public QuestionnaireSearchFields SearchFields { get; set; }
    }

    public class QuestionnaireSearchFields
    {
        public int? QuestionnaireId { get; set; }
    }
}
