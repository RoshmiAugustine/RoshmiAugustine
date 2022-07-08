// -----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public int UserProfileID { get; set; }
        public int UserID { get; set; }
        public long ImageFileID { get; set; }
    }
}
