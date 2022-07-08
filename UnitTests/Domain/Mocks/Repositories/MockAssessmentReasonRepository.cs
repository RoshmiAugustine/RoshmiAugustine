// -----------------------------------------------------------------------
// <copyright file="MockAssessmentReasonRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentReasonRepository : Mock<IAssessmentReasonRepository>
    {
        public MockAssessmentReasonRepository MockGetAllAssessmentReason(List<AssessmentReason> result)
        {
            Setup(x => x.GetAllAssessmentReason()).Returns(result);
            return this;
        }

        public MockAssessmentReasonRepository MockGetAllAssessmentReasonException()
        {
            Setup(x => x.GetAllAssessmentReason()).Throws<Exception>();
            return this;
        }

        public MockAssessmentReasonRepository MockGetAssessmentReason(AssessmentReason result)
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<AssessmentReason, bool>>>()))
                .Returns(Task.FromResult(result));
            return this;
        }
    }
}
