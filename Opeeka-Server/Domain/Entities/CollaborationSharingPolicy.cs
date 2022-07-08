// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingPolicy.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationSharingPolicy : BaseEntity
    {
        public int CollaborationSharingPolicyID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }  
        public int Weight { get; set; }
    }
}
