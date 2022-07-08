// -----------------------------------------------------------------------
// <copyright file="MockQuestionnaireNotifyRiskRuleConditionRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using Opeeka.PICS.Domain.Entities;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireNotifyRiskRuleConditionRepository : Mock<IQuestionnaireNotifyRiskRuleConditionRepository>
    {
        public MockQuestionnaireNotifyRiskRuleConditionRepository MockCloneQuestionnaire(QuestionnaireNotifyRiskRuleConditionsDTO result, List<QuestionnaireNotifyRiskRuleConditionsDTO> resultList)
        {
            Setup(x => x.CloneQuestionnaireNotifyRiskRuleCondition(It.IsAny<List<QuestionnaireNotifyRiskRuleConditionsDTO>>()));

            Setup(x => x.GetQuestionnaireNotifyRiskRuleConditionByRiskRule(It.IsAny<int>()))
                .Returns(resultList);
            return this;
        }

        public MockQuestionnaireNotifyRiskRuleConditionRepository MockGetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(QuestionnaireNotifyRiskRuleCondition result)
        {
            Setup(x => x.GetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(It.IsAny<int>()))
               .Returns(Task.FromResult(result));

            return this;
        }
        public MockQuestionnaireNotifyRiskRuleConditionRepository MockGetRiskItemDetails(List<QuestionnaireRiskItemDetailsDTO> resultList)
        {
            Setup(x => x.GetRiskItemDetails(It.IsAny<int>()))
               .Returns(resultList);
            
            return this;
        }
    }
}
