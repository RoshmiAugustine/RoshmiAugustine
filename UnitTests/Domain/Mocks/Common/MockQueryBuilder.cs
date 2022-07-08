using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Common;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Common
{
    public class MockQueryBuilder : Mock<IQueryBuilder>
    {
        // mock class
        public MockQueryBuilder SetupQueryBuilder(DynamicQueryBuilderDTO returnValue)
        {
            Setup(x => x.BuildQuery(It.IsAny<string>(), It.IsAny<List<QueryFieldMappingDTO>>()))
                             .Returns(returnValue);
            return this;
        }
    }
}
