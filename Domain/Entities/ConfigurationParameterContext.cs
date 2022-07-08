// -----------------------------------------------------------------------
// <copyright file="ConfigurationParameterContext.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.Entities
{
    public class ConfigurationParameterContext : BaseEntity
    {
        public int ConfigurationParameterContextID { get; set; }

        public int ConfigurationParameterID { get; set; }

        public int ConfigurationContextID { get; set; }

        public ConfigurationContext ConfigurationContext { get; set; }
        public ConfigurationParameter ConfigurationParameter { get; set; }
    }
}
