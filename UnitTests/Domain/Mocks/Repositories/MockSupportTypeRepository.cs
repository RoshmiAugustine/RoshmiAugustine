using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockSupportTypeRepository : Mock<ISupportTypeRepository>
    {
        public MockSupportTypeRepository MockSupportTypeList(List<SupportType> results)
        {
            Setup(x => x.GetSupportTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetSupportTypeCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockSupportTypeRepository MockSupportTypeListException(List<SupportType> results)
        {
            Setup(x => x.GetSupportTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetSupportTypeCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockSupportTypeRepository MockAddSupportType(SupportType result)
        {
            Setup(x => x.AddSupportType(It.IsAny<SupportType>()))
                .Returns(result);

            return this;
        }

        public MockSupportTypeRepository MockAddSupportTypeException()
        {
            Setup(x => x.AddSupportType(It.IsAny<SupportType>()))
                 .Throws<Exception>();
            return this;
        }

        public MockSupportTypeRepository MockUpdateSupportType(SupportType result)
        {
            Setup(x => x.UpdateSupportType(It.IsAny<SupportType>()))
               .Returns(result);
            Setup(x => x.GetSupportType(It.IsAny<Int64>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        public MockSupportTypeRepository MockUpdateSupportTypeException()
        {
            Setup(x => x.UpdateSupportType(It.IsAny<SupportType>()))
                .Throws<Exception>();

            Setup(x => x.GetSupportType(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }

        public MockSupportTypeRepository MockdeleteSupportType(SupportType result)
        {
            Setup(x => x.UpdateSupportType(It.IsAny<SupportType>()))
               .Returns(result);

            Setup(x => x.GetSupportType(It.IsAny<Int64>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockSupportTypeRepository MockDeleteSupportTypeException()
        {
            Setup(x => x.UpdateSupportType(It.IsAny<SupportType>()))
                .Throws<Exception>();

            Setup(x => x.GetSupportType(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }

    }
}
