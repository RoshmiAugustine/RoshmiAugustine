
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using System;

namespace Opeeka.PICS.UnitTests.Api.Services
{
  public  class MockLanguageService : Mock<ILanguageService>
    {
        public MockLanguageService MockAddLanguage(CRUDResponseDTO result)
        {
            Setup(x => x.AddLanguage(It.IsAny<LanguageDetailsDTO>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(result);
            return this;
        }
        public MockLanguageService MockAddLanguageArgumentNullException()
        {
            Setup(x => x.AddLanguage(It.IsAny<LanguageDetailsDTO>(), It.IsAny<int>(), It.IsAny<long>()))
                 .Throws<ArgumentNullException>();
            return this;
        }
        public MockLanguageService MockAddLanguageException()
        {
            Setup(x => x.AddLanguage(It.IsAny<LanguageDetailsDTO>(), It.IsAny<int>(), It.IsAny<long>()))
             .Throws<Exception>();
            return this;
        }
        public MockLanguageService MockUpdateLanguage(CRUDResponseDTO result)
        {
            Setup(x => x.UpdateLanguage(It.IsAny<LanguageDetailsDTO>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(result);
            return this;
        }

        public MockLanguageService MockUpdateLanguageException()
        {
            Setup(x => x.UpdateLanguage(It.IsAny<LanguageDetailsDTO>(), It.IsAny<int>(), It.IsAny<long>()))
             .Throws<Exception>();
            return this;
        }

        public MockLanguageService MockDeleteLanguage(CRUDResponseDTO result)
        {
            Setup(x => x.DeleteLanguage( It.IsAny<int>(), It.IsAny<long>()))
                .Returns(result);
            return this;
        }
        public MockLanguageService MockDeleteLanguageException()
        {
            Setup(x => x.DeleteLanguage( It.IsAny<int>(), It.IsAny<long>()))
             .Throws<Exception>();
            return this;
        }

        public MockLanguageService MockGetLanguageList(LanguageListResponseDTO result)
        {
            Setup(x => x.GetLanguageList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(result);
            return this;
        }
        public MockLanguageService MockGetLanguageListException()
        {
            Setup(x => x.GetLanguageList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
              .Throws<Exception>();
            return this;
        }
        

    }
}
