using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Api.Services
{
    public class MockFileUploadService : Mock<IFileService>
    {
        public MockFileUploadService MockSaveProfilePicAsync(IFormFile uploadfile, FileResponseDTO result)
        {
            Setup(x => x.SaveProfilePicAsync(It.IsAny<IFormFile>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<List<string>>()))
                        .ReturnsAsync(result);
            return this;
        }
        public MockFileUploadService MockSaveProfilePicAsyncException(IFormFile uploadfile, FileResponseDTO result)
        {
            Setup(x => x.SaveProfilePicAsync(It.IsAny<IFormFile>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<List<string>>()))
                      .Throws<Exception>();
            return this;
        }
    }
}
