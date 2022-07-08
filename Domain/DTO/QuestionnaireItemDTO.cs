// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireItemDTO
    {
        public int QuestionnaireItemID { get; set; }
        public int QuestionnaireID { get; set; }
        public int ItemID { get; set; }
        public int ListOrder { get; set; }
        public string Name { get; set; }
        public int ResponseValueTypeID { get; set; }
    }
}
