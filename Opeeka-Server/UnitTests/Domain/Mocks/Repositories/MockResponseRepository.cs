// -----------------------------------------------------------------------
// <copyright file="MockAssessmentResponseNoteRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.DTO;
using System.Threading.Tasks;
using System;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockResponseRepository : Mock<IResponseRepository>
    {
        public MockResponseRepository MockGetResponse(ResponseDTO result)
        {
            Setup(x => x.GetResponse(It.IsAny<int>()))
               .Returns(Task.FromResult(result));

            return this;
        }

        public MockResponseRepository MockGetResponseException(ResponseDTO result)
        {
            Setup(x => x.GetResponse(It.IsAny<int>()))
               .Throws<Exception>();

            return this;
        }
    }
}
