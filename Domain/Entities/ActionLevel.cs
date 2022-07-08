// -----------------------------------------------------------------------
// <copyright file="ActionLevel.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class ActionLevel : BaseEntity
    {
        public int ActionLevelID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
    }
}