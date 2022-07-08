using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockIdentifiedGenderRepository : Mock<IIdentifiedGenderRepository>
    {
        public MockIdentifiedGenderRepository MockIdentifiedGenderList(List<IdentifiedGender> results)
        {
            Setup(x => x.GetIdentifiedGenderList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetIdentifiedGenderCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockIdentifiedGenderRepository MockIdentifiedGenderListException()
        {
            Setup(x => x.GetIdentifiedGenderList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetIdentifiedGenderCount(It.IsAny<long>()))
                .Throws<Exception>();
            return this;
        }

        public MockIdentifiedGenderRepository MockAddIdentifiedGender(IdentifiedGender result)
        {
            Setup(x => x.AddIdentifiedGender(It.IsAny<IdentifiedGender>()))
                .Returns(result);
            return this;
        }

        public MockIdentifiedGenderRepository MockAddIdentifiedGenderException()
        {
            Setup(x => x.AddIdentifiedGender(It.IsAny<IdentifiedGender>()))
                 .Throws<Exception>();
            return this;
        }

        public MockIdentifiedGenderRepository MockUpdateIdentifiedGender(IdentifiedGender result)
        {
            Setup(x => x.UpdateIdentifiedGender(It.IsAny<IdentifiedGender>()))
               .Returns(result);
            Setup(x => x.GetIdentifiedGender(It.IsAny<int>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        public MockIdentifiedGenderRepository MockUpdateIdentifiedGenderException()
        {
            Setup(x => x.UpdateIdentifiedGender(It.IsAny<IdentifiedGender>()))
                .Throws<Exception>();
            Setup(x => x.GetIdentifiedGender(It.IsAny<int>()))
                 .Throws<Exception>();
            return this;
        }

        public MockIdentifiedGenderRepository MockGetAllIdentifiedGenderList(List<IdentifiedGender> mockIdentifiedGenderList)
        {
            Setup(x => x.GetIdentifiedGenderList(It.IsAny<int>()))
            .Returns(mockIdentifiedGenderList);
            return this;
        }

        public MockIdentifiedGenderRepository MockGetAllIdentifiedGenderListException()
        {
            Setup(x => x.GetIdentifiedGenderList(It.IsAny<int>()))
           .Throws<Exception>();
            return this;
        }

        public MockIdentifiedGenderRepository MockDeleteIdentifiedGender(IdentifiedGender result)
        {
            Setup(x => x.UpdateIdentifiedGender(It.IsAny<IdentifiedGender>()))
               .Returns(result);

            Setup(x => x.GetIdentifiedGender(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockIdentifiedGenderRepository MockDeleteIdentifiedGenderException(IdentifiedGender result)
        {
            Setup(x => x.UpdateIdentifiedGender(It.IsAny<IdentifiedGender>()))
                .Throws<Exception>();

            Setup(x => x.GetIdentifiedGender(It.IsAny<int>()))
                 .Throws<Exception>();
            return this;
        }
    }
}
