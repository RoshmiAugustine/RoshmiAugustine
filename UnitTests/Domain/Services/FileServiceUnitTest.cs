using System;
using Microsoft.Extensions.Configuration;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class FileServiceUnitTest
    {
        private FileService fileService;
        private Mock<IFileRepository> mockFileRepository;
        public Mock<IConfiguration> mockConfiguration;

        public FileServiceUnitTest()
        {
            this.mockFileRepository = new Mock<IFileRepository>();
            this.mockConfiguration = new Mock<IConfiguration>();
        }

        private void InitialiseUserService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.fileService = new FileService(localize.Object, null, null, this.mockFileRepository.Object, this.mockConfiguration.Object, null, null);
        }


        #region Get File Report
        [Fact]
        public void AddFile_Success_ReturnsCorrectResult()
        {
            this.mockFileRepository = new MockFileRepository().MockAddFile(1);
            InitialiseUserService();
            var result = this.fileService.AddFile(AddMockFile());
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        [Fact]
        public void AddFile_Failure_ExceptionResult()
        {
            this.mockFileRepository = new MockFileRepository().MockAddFileException();
            InitialiseUserService();
            Assert.ThrowsAny<Exception>(() => this.fileService.AddFile(AddMockFile()));
        }

        private FileDTO AddMockFile()
        {
            return new FileDTO()
            {
                AgencyID = 1,
                AzureID = Guid.NewGuid(),
                FileName = "FileName1",
                Path = "temporary/profileImages/1/33/"
            };
        }
        #endregion  Get File Report 
    }
}
