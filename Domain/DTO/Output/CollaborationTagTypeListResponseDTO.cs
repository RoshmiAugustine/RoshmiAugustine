// -----------------------------------------------------------------------
// <copyright file="CollaborationTagTypeListResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class CollaborationTagTypeListResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<CollaborationTagTypeDTO> CollaborationTagTypeList { get; set; }
    }
}
