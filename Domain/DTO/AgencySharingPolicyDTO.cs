// -----------------------------------------------------------------------
// <copyright file="AgencySharingPolicyDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class AgencySharingPolicyDTO
    {
        public int AgencySharingPolicyID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
