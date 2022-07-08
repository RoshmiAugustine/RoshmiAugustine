// -----------------------------------------------------------------------
// <copyright file="AgencySearchDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AgencySearchDTO : BaseSearchDTO
    {
        public int pageNumber { get; set; }
        public int pageLimit { get; set; }

    }
}
