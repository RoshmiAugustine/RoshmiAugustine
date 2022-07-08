using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class CategoryAndItemForSkipLogicResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<SkipLogicItemsDTO> ItemsList { get; set; }
    }
}
