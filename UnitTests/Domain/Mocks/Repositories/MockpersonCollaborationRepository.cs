
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockpersonCollaborationRepository : Mock<IPersonCollaborationRepository>
    {
        public MockpersonCollaborationRepository MockpersonCollaborationAdd(int result)
        {
            Setup(x => x.AddPersonCollaboration(It.IsAny<PersonCollaborationDTO>()))
                 .Returns(result);
            return this;
        }
        public MockpersonCollaborationRepository MockpersonCollaborationEdit(PersonCollaborationDTO result, List<PeopleCollaborationDTO> res)
        {
            Setup(x => x.UpdatePersonCollaboration(It.IsAny<PersonCollaborationDTO>()))
               .Returns(result);

            Setup(x => x.GetPersonCollaboration(It.IsAny<long>()))
                .Returns(res);
            return this;
        }
    }
}
