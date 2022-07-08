// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingPolicyDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationSharingPolicyDTO
    {
        public int CollaborationSharingPolicyID { get; set; }
        public int CollaborationSharingID { get; set; }
        public int SharingPolicyID { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
