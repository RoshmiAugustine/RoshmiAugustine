using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class FileImportInputDTO
    {
        public IFormFile UploadFile { get; set; }
        public int ImportTypeID { get; set; }
        public int? QuestionnaireID { get; set; }
    }
}
