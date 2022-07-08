// -----------------------------------------------------------------------
// <copyright file="NeedMetricsSearchDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class NeedMetricsSearchDTO : BaseSearchDTO
    {
        public Guid? helperIndex { get; set; }
        public int helperID { get; set; }
        public long agencyID { get; set; }
        public List<string> userRole { get; set; }
        public string role { get; set; }
        public bool isSameAsLoggedInUser { get; set; }
        public int userID { get; set; }
    }
}
