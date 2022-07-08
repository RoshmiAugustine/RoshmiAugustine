
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockpersonLanguageRepository : Mock<IPersonLanguageRepository>
    {
        public MockpersonLanguageRepository MockpersonLanguageAdd(long result)
        {
            Setup(x => x.AddPersonLanguage(It.IsAny<PersonLanguageDTO>()))
                 .Returns(result);

            return this;
        }
        public MockpersonLanguageRepository MockpersonLanguageEdit(PersonLanguageDTO result, IReadOnlyList<PersonLanguageDTO> res)
        {
            Setup(x => x.UpdatePersonLanguage(It.IsAny<PersonLanguageDTO>()))
               .Returns(result);

            Setup(x => x.GetPersonLanguage(It.IsAny<long>()))
                .Returns(Task.FromResult(res));
            return this;
        }

    }
}
