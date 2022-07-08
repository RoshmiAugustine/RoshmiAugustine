// -----------------------------------------------------------------------
// <copyright file="ConfigurationParameterDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class ConfigurationParameterDTO
    {
        public String Name { get; set; }
        public String Value { get; set; }
        public String ValueType { get; set; }
        public String Attachment { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
        public bool Deprecated { get; set; }
        public bool CanModify { get; set; }

        public long AgencyId { get; set; }
        public int ConfigurationParameterID { get; set; }
        public int ConfigurationValueTypeID { get; set; }
        public int AttachmentID { get; set; }
        public int ConfigurationID { get; set; }
    }
}
