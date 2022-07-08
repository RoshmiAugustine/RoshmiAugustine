// -----------------------------------------------------------------------
// <copyright file="CategoryDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryName { get; set; }
        public List<QuestionInfoDTO> VersionItems { get; set; }
    }
}
