
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockpersonIdentificationRepository : Mock<IPersonIdentificationRepository>
    {
        public MockpersonIdentificationRepository MockpersonIdentificationAdd(long result)
        {
            Setup(x => x.AddPersonIdentificationType(It.IsAny<PersonIdentificationDTO>()))
                 .Returns(result);

            return this;
        }
        public MockpersonIdentificationRepository MockpersonIdentificationEdit(PersonIdentificationDTO result, IReadOnlyList<PersonIdentificationDTO> res)
        {
            Setup(x => x.UpdatePersonIdentificationType(It.IsAny<PersonIdentificationDTO>()))
               .Returns(result);

            Setup(x => x.GetPersonIdentificationType(It.IsAny<long>()))
                .Returns(Task.FromResult(res));
            return this;
        }

    }
}
