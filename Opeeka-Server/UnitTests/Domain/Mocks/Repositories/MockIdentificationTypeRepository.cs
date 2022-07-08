using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockIdentificationTypeRepository : Mock<IIdentificationTypeRepository>
    {
        public MockIdentificationTypeRepository MockIdentificationTypeList(List<IdentificationType> results)
        {
            Setup(x => x.GetIdentificationTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetIdentificationTypeCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockIdentificationTypeRepository MockIdentificationTypeListException(List<IdentificationType> results)
        {
            Setup(x => x.GetIdentificationTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetIdentificationTypeCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockIdentificationTypeRepository MockAddIdentificationType(IdentificationType result)
        {
            Setup(x => x.AddIdentificationType(It.IsAny<IdentificationType>()))
                .Returns(result);

            return this;
        }

        public MockIdentificationTypeRepository MockAddIdentificationTypeException()
        {
            Setup(x => x.AddIdentificationType(It.IsAny<IdentificationType>()))
                 .Throws<Exception>();
            return this;
        }

        public MockIdentificationTypeRepository MockUpdateIdentificationType(IdentificationType result)
        {
            Setup(x => x.UpdateIdentificationType(It.IsAny<IdentificationType>()))
               .Returns(result);
            Setup(x => x.GetIdentificationType(It.IsAny<Int64>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        public MockIdentificationTypeRepository MockUpdateIdentificationTypeException()
        {
            Setup(x => x.UpdateIdentificationType(It.IsAny<IdentificationType>()))
                .Throws<Exception>();

            Setup(x => x.GetIdentificationType(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }

        public MockIdentificationTypeRepository MockdeleteIdentificationType(IdentificationType result)
        {
            Setup(x => x.UpdateIdentificationType(It.IsAny<IdentificationType>()))
               .Returns(result);

            Setup(x => x.GetIdentificationType(It.IsAny<Int64>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockIdentificationTypeRepository MockDeleteIdentificationTypeException()
        {
            Setup(x => x.UpdateIdentificationType(It.IsAny<IdentificationType>()))
                .Throws<Exception>();

            Setup(x => x.GetIdentificationType(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }
    }
}
