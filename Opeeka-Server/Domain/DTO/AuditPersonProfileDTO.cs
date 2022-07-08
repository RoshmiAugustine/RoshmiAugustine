// -----------------------------------------------------------------------
// <copyright file="AuditPersonProfileDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class AuditPersonProfileDTO
    {
        public int AuditPersonProfileID { get; set; }
        public string TypeName { get; set; }
        public long ParentID { get; set; }
        public int ChildRecordID { get; set; }
        public string ChildRecordName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
    }
}
