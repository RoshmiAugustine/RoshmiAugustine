using System;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockFileRepository : Mock<IFileRepository>
    {
        public MockFileRepository MockAddFile(long result)
        {
            Setup(x => x.AddFile(It.IsAny<FileDTO>()))
                .Returns(result);
            return this;
        }
        public Mock<IFileRepository> MockAddFileException()
        {
            Setup(x => x.AddFile(It.IsAny<FileDTO>()))
           .Throws<Exception>();
            return this;
        }
    }
}
