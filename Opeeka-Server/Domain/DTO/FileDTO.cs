// -----------------------------------------------------------------------
// <copyright file="FileDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class FileDTO
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
