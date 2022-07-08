// -----------------------------------------------------------------------
// <copyright file="MockVoiceTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockVoiceTypeRepository : Mock<IVoiceTypeRepository>
    {
        public MockVoiceTypeRepository MockGetAllVoiceType(List<VoiceType> result)
        {
            Setup(x => x.GetAllVoiceType()).Returns(result);
            return this;
        }

        public MockVoiceTypeRepository MockGetAllVoiceTypeException()
        {
            Setup(x => x.GetAllVoiceType()).Throws<Exception>();
            return this;
        }

        public MockVoiceTypeRepository MockGetAllVoiceTypeInDetail(List<VoiceTypeDTO> result)
        {
            Setup(x => x.GetAllVoiceTypeInDetail(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>())).Returns(result);
            return this;
        }

        public MockVoiceTypeRepository MockGetAllVoiceTypeInDetailException()
        {
            Setup(x => x.GetAllVoiceTypeInDetail(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>())).Throws<Exception>();
            return this;
        }
        public MockVoiceTypeRepository MockGetVoiceType(VoiceType result)
        {
            Setup(x => x.GetVoiceType(It.IsAny<int>())).Returns(result);
            return this;
        }
    }
}
