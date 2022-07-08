// -----------------------------------------------------------------------
// <copyright file="MockAssessmentResponseNoteRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentResponseNoteRepository : Mock<IAssessmentResponseNoteRepository>
    {
        public MockAssessmentResponseNoteRepository MockAddAssessmentProgress(AssessmentResponseNote result)
        {
            Setup(x => x.AddAssessmentResponseNote(It.IsAny<AssessmentResponseNote>()))
               .Returns(result);
            Setup(x => x.AddBulkAssessmentResponseNote(It.IsAny<List<AssessmentResponseNote>>()));
            return this;
        }
    }
}
