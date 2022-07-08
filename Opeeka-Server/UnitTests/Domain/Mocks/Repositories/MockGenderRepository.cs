using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockGenderRepository : Mock<IGenderRepository>
    {
        public MockGenderRepository MockGenderList(List<GenderDTO> results)
        {
            Setup(x => x.GetGenderList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetGenderCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockGenderRepository MockGenderListException(List<GenderDTO> results)
        {
            Setup(x => x.GetGenderList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetGenderCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockGenderRepository MockAddGender(GenderDTO result)
        {
            Setup(x => x.AddGender(It.IsAny<GenderDTO>()))
                .Returns(result);
            return this;
        }

        public MockGenderRepository MockAddGenderException(GenderDTO result)
        {
            Setup(x => x.AddGender(It.IsAny<GenderDTO>()))
                 .Throws<Exception>();
            return this;
        }

        public MockGenderRepository MockUpdateGender(GenderDTO result)
        {
            Setup(x => x.UpdateGender(It.IsAny<GenderDTO>()))
               .Returns(result);
            Setup(x => x.GetGender(It.IsAny<Int64>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        public MockGenderRepository MockUpdateGenderException(GenderDTO result)
        {
            Setup(x => x.UpdateGender(It.IsAny<GenderDTO>()))
                .Throws<Exception>();

            Setup(x => x.GetGender(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }

        public MockGenderRepository MockdeleteGender(GenderDTO result)
        {
            Setup(x => x.UpdateGender(It.IsAny<GenderDTO>()))
               .Returns(result);

            Setup(x => x.GetGender(It.IsAny<Int64>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockGenderRepository MockDeleteGenderException(GenderDTO result)
        {
            Setup(x => x.UpdateGender(It.IsAny<GenderDTO>()))
                .Throws<Exception>();

            Setup(x => x.GetGender(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }
    }
}
