// -----------------------------------------------------------------------
// <copyright file="GetRUCollaborationListDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class GetRUCollaborationListDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<RUCollaborationDataDTO> CollaborationList { get; set; }
    }
}
