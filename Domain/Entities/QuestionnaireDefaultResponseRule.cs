﻿// -----------------------------------------------------------------------
// <copyright file="QuestionnaireDefaultResponseRule.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireDefaultResponseRule : BaseEntity
    {
        public int QuestionnaireDefaultResponseRuleID { get; set; }
        public string Name { get; set; }
        public int QuestionnaireID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? ClonedQuestionnaireDefaultResponseRuleID { get; set; }


        public Questionnaire Questionnaire { get; set; }
        public User UpdateUser { get; set; }
    }
}
