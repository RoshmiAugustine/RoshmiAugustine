using System;
using System.Collections.Generic;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockSearchRepository : Mock<ISearchRepository>
    {
        internal Mock<ISearchRepository> MockUpperpaneSearchResults(List<UpperpaneSearchDTO> mockUpperpaneSearch)
        {
            Setup(x => x.GetUpperpaneSearchResults(It.IsAny<UpperpaneSearchKeyDTO>(), It.IsAny<string>(), It.IsAny<Int64>(), It.IsAny<Int32>(), It.IsAny<string>()))
            .Returns(mockUpperpaneSearch);
            return this;
        }

        internal Mock<ISearchRepository> MockUpperpaneSearchException()
        {
            Setup(x => x.GetUpperpaneSearchResults(It.IsAny<UpperpaneSearchKeyDTO>(), It.IsAny<string>(), It.IsAny<Int64>(), It.IsAny<Int32>(), It.IsAny<string>()))
                .Throws<Exception>();
            return this;
        }
    }
}
