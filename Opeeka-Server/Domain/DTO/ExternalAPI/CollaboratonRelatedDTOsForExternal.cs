using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class CollaborationSearchInputDTO : Paginate
    {
        public CollaboratioSearchFields SearchFields { get; set; }
    }
    public class CollaboratioSearchFields
    {
        public Guid? CollaborationIndex { get; set; }

        public Guid? PersonIndex { get; set; }

        public string Name { get; set; }
        public int? CollaborationId { get; set; }
    }
    public class CollaborationResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<CollaborationDetailsListDTO> collaborationDetailsListDTO { get; set; }
    }
    public class CollaborationDetailsListDTO
    {
        public int CollaborationID { get; set; }

        public Guid CollaborationIndex { get; set; }

        public int TherapyTypeID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        public int UpdateUserID { get; set; }

        public long AgencyID { get; set; }

        public int CollaborationLevelID { get; set; }

        public string Code { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public string Questionnaires { get; set; }

        public string Categories { get; set; }

        public string Leads { get; set; }

        public int TotalCount { get; set; }
    }
}
