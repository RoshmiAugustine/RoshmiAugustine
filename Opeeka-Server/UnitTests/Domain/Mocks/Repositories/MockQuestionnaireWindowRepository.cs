// -----------------------------------------------------------------------
// <copyright file="MockQuestionnaireWindowRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireWindowRepository : Mock<IQuestionnaireWindowRepository>
    {
        public MockQuestionnaireWindowRepository MockCloneQuestionnaire(QuestionnaireWindowsDTO result, List<QuestionnaireWindowsDTO> resultList)
        {
            Setup(x => x.CloneQuestionnaireWindow(It.IsAny<List<QuestionnaireWindowsDTO>>()));

            Setup(x => x.GetQuestionnaireWindowsByQuestionnaire(It.IsAny<int>()))
                .Returns(resultList);
            return this;
        }
    }
}
