// -----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
namespace Opeeka.PICS.Domain.Entities
{
    public class Configuration : BaseEntity
    {
        public int ConfigurationID { get; set; }

        public String Value { get; set; }

        public int ContextFKValue { get; set; }

        public int ConfigurationParameterContextID { get; set; }

        public ConfigurationParameterContext ConfigurationParameterContext { get; set; }
    }
}
