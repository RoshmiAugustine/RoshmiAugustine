// -----------------------------------------------------------------------
// <copyright file="NotificationMode.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class NotificationMode : BaseEntity
    {
        public int NotificationModeID { get; set; }
        public string Name { get; set; }
    }
}
