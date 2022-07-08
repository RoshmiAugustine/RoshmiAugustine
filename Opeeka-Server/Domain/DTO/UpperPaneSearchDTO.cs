// -----------------------------------------------------------------------
// <copyright file="UpperPaneSearchDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class UpperpaneSearchDTO
    {
        public Int64 Id { get; set; }
        public Guid Index { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsSharedPerson { get; set; }
        public int TotalCount { get; set; }
        public int IsActive { get; set; }
        public int IsRemoved { get; set; }
        public string AgencyName { get; set; }
        public string EmailId { get; set; }

    }
}
