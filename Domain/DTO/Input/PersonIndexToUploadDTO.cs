using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class PersonIndexToUploadDTO
    {
        public long AgencyID { get; set; }
        public List<Guid> PersonIndexesToUpload { get; set; }
    }
}
