// -----------------------------------------------------------------------
// <copyright file="MockAssessmentStatusRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentStatusRepository : Mock<IAssessmentStatusRepository>
    {
        public MockAssessmentStatusRepository MockAddAssessmentProgress(AssessmentStatus result)
        {
            Setup(x => x.GetAssessmentStatus(It.IsAny<string>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockAssessmentStatusRepository MockGetAssessmentStatus(AssessmentStatus result)
        {
            Setup(x => x.GetAssessmentStatusDetails(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }
        public MockAssessmentStatusRepository GetAssessmentStatus(AssessmentStatus result)
        {
            Setup(x => x.GetAssessmentStatus(It.IsAny<string>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        internal Mock<IAssessmentStatusRepository> MockGetAllAssessmentStatus(List<AssessmentStatus> lists)
        {
            Setup(x => x.GetAllAssessmentStatus())
                .Returns(lists);
            return this;
        }
    }
}
