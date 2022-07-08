// -----------------------------------------------------------------------
// <copyright file="MockQuestionnaireNotifyRiskScheduleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireNotifyRiskScheduleRepository : Mock<IQuestionnaireNotifyRiskScheduleRepository>
    {
        public MockQuestionnaireNotifyRiskScheduleRepository MockCloneQuestionnaire(QuestionnaireNotifyRiskSchedulesDTO result)
        {
            Setup(x => x.CloneQuestionnaireNotifyRiskSchedule(It.IsAny<QuestionnaireNotifyRiskSchedulesDTO>()))
               .Returns(result);

            Setup(x => x.GetQuestionnaireNotifyRiskScheduleByQuestionnaire(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }
    }
}
