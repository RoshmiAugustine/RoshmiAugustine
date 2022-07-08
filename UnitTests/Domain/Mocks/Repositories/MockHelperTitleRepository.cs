using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockHelperTitleRepository : Mock<IHelperTitleRepository>
    {
        public MockHelperTitleRepository MockAddHelperTitle(HelperTitleDTO result)
        {
            Setup(x => x.AddHelperTitle(It.IsAny<HelperTitleDTO>()))
                .Returns(result);
            return this;
        }

        public MockHelperTitleRepository MockUpdateHelperTitle(HelperTitleDTO result)
        {
            Setup(x => x.UpdateHelperTitle(It.IsAny<HelperTitleDTO>()))
                .Returns(result);
            return this;
        }

        public MockHelperTitleRepository MockGetHelperTitleList(List<HelperTitleDTO> result, long agencyId = 0)
        {
            if (agencyId == 0)
            {
                Setup(x => x.GetHelperTitleList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                    .Returns(result);
                return this;
            }
            else
            {
                Setup(x => x.GetHelperTitleList(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(result);
                return this;
            }
        }

        public MockHelperTitleRepository MockGetHelperTitle(HelperTitleDTO result)
        {
            Setup(x => x.GetHelperTitle(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }

        public MockHelperTitleRepository MockGetHelperTitleCount()
        {
            Setup(x => x.GetHelperTitleCount(1))
                .Returns(0);
            return this;
        }

        public MockHelperTitleRepository MockAddHelperTitleFailure(HelperTitleDTO result)
        {
            Setup(x => x.AddHelperTitle(It.IsAny<HelperTitleDTO>()))
                .Returns(new HelperTitleDTO() { HelperTitleID = 0 });
            return this;
        }

        public MockHelperTitleRepository MockUpdateHelperTitleFailure(HelperTitleDTO result)
        {
            Setup(x => x.UpdateHelperTitle(It.IsAny<HelperTitleDTO>()))
                .Returns((HelperTitleDTO)null);
            return this;
        }

        public MockHelperTitleRepository MockGetHelperTitleException(HelperTitleDTO result)
        {
            Setup(x => x.GetHelperTitle(It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

        public MockHelperTitleRepository MockHelperTitleListException(List<HelperTitleDTO> results, long agencyId = 0)
        {
            if (agencyId == 0)
            {
                Setup(x => x.GetHelperTitleList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
                Setup(x => x.GetHelperTitleCount(1))
                    .Returns(results.Count);
                return this;
            }
            else
            {
                Setup(x => x.GetHelperTitleList(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
                Setup(x => x.GetHelperTitleCount(1))
                    .Returns(results.Count);
                return this;
            }
        }

        public MockHelperTitleRepository MockAddHelperTitleException(HelperTitleDTO result)
        {
            Setup(x => x.AddHelperTitle(It.IsAny<HelperTitleDTO>()))
                .Throws<Exception>();
            return this;
        }

        public MockHelperTitleRepository MockUpdateHelperTitleException(HelperTitleDTO result)
        {
            Setup(x => x.UpdateHelperTitle(It.IsAny<HelperTitleDTO>()))
                .Throws<Exception>();
            return this;
        }
    }
}
