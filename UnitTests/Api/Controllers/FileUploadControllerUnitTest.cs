using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Api.Controllers;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Api.Common;
using Opeeka.PICS.UnitTests.Api.Services;
using System.IO;
using Xunit;

namespace Opeeka.PICS.UnitTests.Api.Controllers
{
    public class FileUploadControllerUnitTest
    {
        /// Initializes a new instance of the fileservice/> class.
        private Mock<IFileService> mockfileService;

        /// Initializes a new instance of the <see cref="configuration"/> class.
        private Mock<IConfiguration> mockConfiguration;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<FileUploadController>> mockLogger;

        private FileUploadController fileUploadController;

        private MockUserIdentity mockUserIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadController"/> class.
        /// </summary>
        /// 
        public FileUploadControllerUnitTest()
        {
            this.mockLogger = new Mock<ILogger<FileUploadController>>();
            this.mockUserIdentity = new MockUserIdentity();
            this.mockConfiguration = new Mock<IConfiguration>();
        }
        private void InitialiseFileUploadService()
        {
            this.fileUploadController = new FileUploadController(this.mockLogger.Object, this.mockConfiguration.Object, this.mockfileService.Object);
            this.fileUploadController.ControllerContext = mockUserIdentity.MockGetClaimsIdentity();
        }

        //-------------------------------------------------------- SaveProfilePic--------------------------------------------------------
        [Fact]
        public void SaveProfilePicAsync_Success_Result()
        {
            this.mockfileService = new MockFileUploadService().MockSaveProfilePicAsync(CreateMockFile(), GetMockFileResponseDTO());

            InitialiseFileUploadService();
            var result = this.fileUploadController.SaveProfilePicAsync(CreateMockFile()).Result;

            Assert.NotNull(result);
            Assert.IsType<FileResponseDTO>(result.Value);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        //-------------------------------------------------------- SaveProfilePic--------------------------------------------------------
        [Fact]
        public void SaveProfilePicAsync_Exception_Result()
        {
            var mockFileResponseDTO = new FileResponseDTO();
            mockFileResponseDTO.fileID = 0;
            this.mockfileService = new MockFileUploadService().MockSaveProfilePicAsyncException(CreateMockFile(), mockFileResponseDTO);

            InitialiseFileUploadService();
            var result = (ObjectResult)(this.fileUploadController.SaveProfilePicAsync(CreateMockFile()).Result.Result);

            Assert.Equal("An error occurred while adding profile image. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        }

        private FileResponseDTO GetMockFileResponseDTO()
        {
            return new FileResponseDTO
            {
                ResponseStatusCode = PCISEnum.StatusCodes.Success,
                ResponseStatus = PCISEnum.StatusMessages.Success,
                fileID = 1
            };
        }
        private IFormFile CreateMockFile()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var file = fileMock.Object;
            return file;
        }
    }
}
