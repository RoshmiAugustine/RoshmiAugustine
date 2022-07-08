// -----------------------------------------------------------------------
// <copyright file="EditSexualityDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class EditSexualityDTO
    {
        public long SexualityID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }

    }
}
