// -----------------------------------------------------------------------
// <copyright file="CollaborationSearchDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class CollaborationSearchDTO : BaseSearchDTO
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
