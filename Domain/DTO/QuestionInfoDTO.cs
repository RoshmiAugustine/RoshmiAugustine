// -----------------------------------------------------------------------
// <copyright file="QuestionInfoDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionInfoDTO
    {
        public int QuestionnaireItemID { get; set; }

        public int ItemId { get; set; }
        public string Title { get; set; }
        public int ListOrder { get; set; }
        public string Description { get; set; }
        public string Property { get; set; }

        public bool EditableMin { get; set; }
        public bool Editable { get; set; }

        public bool eEitableMax { get; set; }

        public int MinThreshold { get; set; }
        public int MaxThreshold { get; set; }

        public string MinTypeInfo { get; set; }

        public bool Required { get; set; }

        public string DefaultTypeInfo { get; set; }

        public string MaxTypeInfo { get; set; }
        public List<ValueInfoDTO> Responses { get; set; }
    }
}
