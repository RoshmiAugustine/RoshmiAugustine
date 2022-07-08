// -----------------------------------------------------------------------
// <copyright file="MockQuestionnaireReminderRuleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireReminderRuleRepository : Mock<IQuestionnaireReminderRuleRespository>
    {
        public MockQuestionnaireReminderRuleRepository MockCloneQuestionnaire(QuestionnaireReminderRulesDTO result, List<QuestionnaireReminderRulesDTO> resultList)
        {
            Setup(x => x.CloneQuestionnaireReminderRule(It.IsAny<List<QuestionnaireReminderRulesDTO>>()));

            Setup(x => x.GetQuestionnaireReminderRulesByQuestionnaire(It.IsAny<int>()))
                .Returns(resultList);
            return this;
        }
    }
}
