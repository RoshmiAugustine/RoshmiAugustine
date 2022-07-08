// -----------------------------------------------------------------------
// <copyright file="PersonSharingDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonSharedDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }

        public bool IsShared { get; set; }
        public Guid AgencySharingIndex { get; set; }
        public Guid CollaborationSharingIndex { get; set; }
        public bool IsActiveForSharing { get; set; }
    }
}
