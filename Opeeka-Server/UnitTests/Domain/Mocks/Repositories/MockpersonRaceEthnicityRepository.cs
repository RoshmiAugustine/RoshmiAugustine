
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockpersonRaceEthnicityRepository : Mock<IPersonRaceEthnicityRepository>
    {
        public MockpersonRaceEthnicityRepository MockpersonRaceEthnicityADD(long result)
        {
            Setup(x => x.AddRaceEthnicity(It.IsAny<PersonRaceEthnicityDTO>()))
                 .Returns(result);

            return this;
        }

        public MockpersonRaceEthnicityRepository MockpersonRaceEthnicityEdit(PersonRaceEthnicityDTO result, IReadOnlyList<PersonRaceEthnicityDTO> res)
        {
            Setup(x => x.UpdateRaceEthnicity(It.IsAny<PersonRaceEthnicityDTO>()))
               .Returns(result);

            Setup(x => x.GetRaceEthnicity(It.IsAny<long>()))
                  .Returns(Task.FromResult(res));
            return this;
        }
    }
}
