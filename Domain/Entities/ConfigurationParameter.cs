// -----------------------------------------------------------------------
// <copyright file="ConfigurationParameter.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class ConfigurationParameter : BaseEntity
    {
        public int ConfigurationParameterID { get; set; }

        public int ConfigurationValueTypeID { get; set; }
        public int AgencyId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public bool IsActive { get; set; }

        public bool Deprecated { get; set; }

        public bool CanModify { get; set; }
        public ConfigurationValueType ConfigurationValueType { get; set; }

    }
}
