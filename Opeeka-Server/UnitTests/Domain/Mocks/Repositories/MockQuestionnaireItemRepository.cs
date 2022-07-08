// -----------------------------------------------------------------------
// <copyright file="MockQuestionnaireItemRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireItemRepository : Mock<IQuestionnaireItemRepository>
    {
        public MockQuestionnaireItemRepository MockCloneQuestionnaire(QuestionnaireItemsDTO result, List<QuestionnaireItemsDTO> resultList)
        {
            Setup(x => x.CloneQuestionnaireItem(It.IsAny<List<QuestionnaireItemsDTO>>()));

            Setup(x => x.GetQuestionnaireItemsByQuestionnaire(It.IsAny<int>()))
                .Returns(resultList);
            return this;
        }
    }
}
