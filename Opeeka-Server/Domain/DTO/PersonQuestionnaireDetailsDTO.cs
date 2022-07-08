// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnaireDetailsDTO
    {
        public Guid PersonIndex { get; set; }
        public int QuestionnaireID { get; set; }
    }
}
