// -----------------------------------------------------------------------
// <copyright file="ConfigurationContext.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
namespace Opeeka.PICS.Domain.Entities
{
    public class ConfigurationContext : BaseEntity
    {
        public int ConfigurationContextID { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int? ParentContextID { get; set; }

        public String EntityName { get; set; }

        public bool FKValueRequired { get; set; }

        public ConfigurationContext ParentContext { get; set; }
    }
}
