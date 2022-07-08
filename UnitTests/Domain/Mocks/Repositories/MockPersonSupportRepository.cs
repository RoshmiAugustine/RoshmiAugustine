
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockPersonSupportRepository : Mock<IPersonSupportRepository>
    {
        public MockPersonSupportRepository MockPeopleSupport(int result)
        {
            Setup(x => x.AddPersonSupport(It.IsAny<PersonSupportDTO>()))
                 .Returns(result);

            return this;
        }
        public MockPersonSupportRepository MockPeopleSupportEdit(PersonSupportDTO result, List<PersonSupportDTO> res)
        {
            Setup(x => x.UpdatePersonSupport(It.IsAny<PersonSupportDTO>()))
               .Returns(result);

            Setup(x => x.GetPersonSupport(It.IsAny<long>()))
                .Returns(res);
            return this;
        }
        public MockPersonSupportRepository MockPeopleSupportDetails(PersonSupport result)
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<PersonSupport, bool>>>()))
                 .Returns(Task.FromResult(result));

            return this;
        }
        public MockPersonSupportRepository MockPeopleSupportDetailsException(PersonSupport result)
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<PersonSupport, bool>>>()))
              .Throws<Exception>();
            return this;
        }
    }
}
