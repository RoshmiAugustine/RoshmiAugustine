using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockHelperRepository : Mock<IHelperRepository>
    {
        public MockHelperRepository MockGetHelperCountByHelperTitle(int helperTitleId, int count)
        {
            Setup(x => x.GetHelperCountByHelperTitle(helperTitleId))
                .Returns(count);
            return this;
        }
        public MockHelperRepository MockUserDetails(UserDetailsDTO results)
        {
            Setup(x => x.GetUserDetails(It.IsAny<Int64>()))
             .Returns(results);

            return this;

        }
        public MockHelperRepository MockUserDetailsException(UserDetailsDTO results)
        {
            Setup(x => x.GetUserDetails(It.IsAny<Int64>()))
                .Throws<Exception>();
            return this;
        }


        public MockHelperRepository MockGetAllManager(List<Helper> result)
        {
            Setup(x => x.GetAllManager(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(result);
            return this;
        }

        public MockHelperRepository MockGetAllManagerException()
        {
            Setup(x => x.GetAllManager(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Throws<Exception>();
            return this;
        }

        internal Mock<IHelperRepository> MockRemoveHelper(Helper mockHelper, HelperDTO mockHelperDTO)
        {
            Setup(x => x.GetHelperByIndexAsync(It.IsAny<Guid>())).Returns(Task.FromResult(mockHelper));
            Setup(x => x.GetHelperUsedCount(It.IsAny<int>())).Returns(0);
            Setup(x => x.UpdateHelper(It.IsAny<Helper>())).Returns(mockHelperDTO);
            return this;
        }

        internal Mock<IHelperRepository> MockRemoveHelperException(Helper mockHelper, HelperDTO mockHelperDTO)
        {
            Setup(x => x.GetHelperByIndexAsync(It.IsAny<Guid>())).Throws<Exception>();
            return this;
        }
    }
}
