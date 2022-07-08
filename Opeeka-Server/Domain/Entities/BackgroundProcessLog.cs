// -----------------------------------------------------------------------
// <copyright file="NotifyReminder.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class BackgroundProcessLog : BaseEntity
    {
        public int BackgroundProcessLogID { get; set; }

        public string ProcessName { get; set; }

        public DateTime LastProcessedDate { get; set; }

    }
}
