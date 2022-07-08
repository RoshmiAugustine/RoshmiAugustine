// -----------------------------------------------------------------------
// <copyright file="ConfigurationValueType.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Entities
{
    public class ConfigurationValueType : BaseEntity
    {
        public int ConfigurationValueTypeID { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public ICollection<ConfigurationParameter> ConfigurationParameters { get; set; }

    }
}
