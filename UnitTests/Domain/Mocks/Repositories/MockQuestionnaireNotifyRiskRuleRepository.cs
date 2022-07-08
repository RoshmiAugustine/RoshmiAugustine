// -----------------------------------------------------------------------
// <copyright file="MockQuestionnaireNotifyRiskRuleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireNotifyRiskRuleRepository : Mock<IQuestionnaireNotifyRiskRuleRepository>
    {
        public MockQuestionnaireNotifyRiskRuleRepository MockCloneQuestionnaire(QuestionnaireNotifyRiskRulesDTO result, List<QuestionnaireNotifyRiskRulesDTO> resultList)
        {
            Setup(x => x.CloneQuestionnaireNotifyRiskRule(It.IsAny<List<QuestionnaireNotifyRiskRulesDTO>>()));

            Setup(x => x.GetQuestionnaireNotifyRiskRuleBySchedule(It.IsAny<int>()))
                .Returns(resultList);
            return this;
        }
    }
}
