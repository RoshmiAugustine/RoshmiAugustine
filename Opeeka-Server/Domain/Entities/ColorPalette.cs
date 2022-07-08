// -----------------------------------------------------------------------
// <copyright file="ColorPalette.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class ColorPalette
    {
        public int ColorPaletteID { get; set; }
        public string Name { get; set; }
        public string RGB { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public User UpdateUser { get; set; }
    }
}
