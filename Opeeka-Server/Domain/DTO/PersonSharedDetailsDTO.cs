// -----------------------------------------------------------------------
// <copyright file="PersonSharingDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonSharingDetailsDTO
    {
        public int AgencySharingWeight { get; set; }
        public int CollaborationSharingWeight { get; set; }
        public bool HistoricalView { get; set; }
        public bool IsShared { get; set; }
        public Guid AgencySharingIndex { get; set; }
        public Guid CollaborationSharingIndex { get; set; }
        public bool IsActiveForSharing { get; set; }
    }
}
