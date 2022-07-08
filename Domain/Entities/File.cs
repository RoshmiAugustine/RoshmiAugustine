// -----------------------------------------------------------------------
// <copyright file="File.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class File : BaseEntity
    {
        public long FileID { get; set; }
        public long AgencyID { get; set; }
        public string FileName { get; set; }
        public string AzureFileName { get; set; }
        public string Path { get; set; }
        public Guid AzureID { get; set; }
        public bool IsTemp { get; set; }
    }
}
