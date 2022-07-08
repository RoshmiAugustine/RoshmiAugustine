// -----------------------------------------------------------------------
// <copyright file="SharingPolicy.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class SharingPolicy : BaseEntity
    {
        public int SharingPolicyID { get; set; }

        public string AccessName { get; set; }
    }
}