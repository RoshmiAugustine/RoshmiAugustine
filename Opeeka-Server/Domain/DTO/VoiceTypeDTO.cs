// -----------------------------------------------------------------------
// <copyright file="VoiceTypeDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class VoiceTypeDTO
    {
        public int VoiceTypeID { get; set; }
        public string VoiceTypeName { get; set; }
        public string NameInDetail { get; set; }
        public long? FkIDValue { get; set; }
        public bool IsRemoved { get; set; }
        public string Email { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public int HelperId { get; set; }
    }
}
