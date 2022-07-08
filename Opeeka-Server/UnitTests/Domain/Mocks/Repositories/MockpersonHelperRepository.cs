
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockpersonHelperRepository : Mock<IPersonHelperRepository>
    {
        public MockpersonHelperRepository MockpersonHelperAdd(long res)
        {
            Setup(x => x.AddPersonHelper(It.IsAny<PersonHelperDTO>()))
                 .Returns(res);
            return this;
        }
        public MockpersonHelperRepository MockpersonHelperEdit(PersonHelperDTO result, IReadOnlyList<PersonHelperDTO> res)
        {
            Setup(x => x.UpdatePersonHelper(It.IsAny<PersonHelperDTO>()))
               .Returns(result);

            Setup(x => x.GetPersonHelper(It.IsAny<long>()))
                .Returns(Task.FromResult(res));
            return this;
        }
    }
}
