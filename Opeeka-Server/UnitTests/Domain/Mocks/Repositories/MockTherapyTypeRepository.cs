using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockTherapyTypeRepository : Mock<ITherapyTypeRepository>
    {
        public MockTherapyTypeRepository MockAddTherapyType(TherapyTypeDTO result)
        {
            Setup(x => x.AddTherapyType(It.IsAny<TherapyTypeDTO>()))
                .Returns(result);
            return this;
        }

        public MockTherapyTypeRepository MockUpdateTherapyType(TherapyTypeDTO result)
        {
            Setup(x => x.UpdateTherapyType(It.IsAny<TherapyTypeDTO>()))
                .Returns(result);
            return this;
        }

        public MockTherapyTypeRepository MockGetTherapyTypeList(List<TherapyTypeDTO> result, long agencyId = 0)
        {
            if (agencyId == 0)
            {
                Setup(x => x.GetTherapyTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                    .Returns(result);
                return this;
            }
            else
            {
                Setup(x => x.GetTherapyTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(result);
                return this;
            }
        }

        public MockTherapyTypeRepository MockGetTherapyType(TherapyTypeDTO result)
        {
            Setup(x => x.GetTherapyType(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }

        public MockTherapyTypeRepository MockGetTherapyTypeCount()
        {
            Setup(x => x.GetTherapyTypeCount(It.IsAny<long>()))
                .Returns(0);
            return this;
        }

        public MockTherapyTypeRepository MockAddTherapyTypeFailure(TherapyTypeDTO result)
        {
            Setup(x => x.AddTherapyType(It.IsAny<TherapyTypeDTO>()))
                .Returns(new TherapyTypeDTO() { TherapyTypeID = 0 });
            return this;
        }

        public MockTherapyTypeRepository MockUpdateTherapyTypeFailure(TherapyTypeDTO result)
        {
            Setup(x => x.UpdateTherapyType(It.IsAny<TherapyTypeDTO>()))
                .Returns((TherapyTypeDTO)null);
            return this;
        }

        public MockTherapyTypeRepository MockGetTherapyTypeException(TherapyTypeDTO result)
        {
            Setup(x => x.GetTherapyType(It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

        public MockTherapyTypeRepository MockTherapyTypeListException(List<TherapyTypeDTO> results, long agencyId = 0)
        {
            if (agencyId == 0)
            {
                Setup(x => x.GetTherapyTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
                Setup(x => x.GetTherapyTypeCount(It.IsAny<long>()))
                    .Returns(results.Count);
                return this;
            }
            else
            {
                Setup(x => x.GetTherapyTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
                Setup(x => x.GetTherapyTypeCount(It.IsAny<long>()))
                    .Returns(results.Count);
                return this;
            }
        }

        public MockTherapyTypeRepository MockAddTherapyTypeException(TherapyTypeDTO result)
        {
            Setup(x => x.AddTherapyType(It.IsAny<TherapyTypeDTO>()))
                .Throws<Exception>();
            return this;
        }

        public MockTherapyTypeRepository MockUpdateTherapyTypeException(TherapyTypeDTO result)
        {
            Setup(x => x.UpdateTherapyType(It.IsAny<TherapyTypeDTO>()))
                .Throws<Exception>();
            return this;
        }
    }
}
