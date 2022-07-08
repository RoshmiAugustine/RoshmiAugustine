// -----------------------------------------------------------------------
// <copyright file="CollaborationTag.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationTag : BaseEntity
    {
        public int CollaborationTagID { get; set; }
        public int CollaborationID { get; set; }
        public int CollaborationTagTypeID { get; set; }
        public bool IsRemoved { get; set; }

        public Collaboration Collaboration { get; set; }
        public CollaborationTagType CollaborationTagType { get; set; }
    }
}
