using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockImportRepository : Mock<IImportRepository>
    {


        public MockImportRepository MockImportFile(int v, List<ImportType> mockFileImport)
        {
            Setup(x => x.GetAllImportTypes(It.IsAny<long>()))
         .Returns(mockFileImport);
            Setup(x => x.InsertImportFileDetails(It.IsAny<FileImportDTO>()))
          .Returns(v);
            return this;
        }

        public MockImportRepository MockImportFileException()
        {
            Setup(x => x.InsertImportFileDetails(It.IsAny<FileImportDTO>()))
               .Throws<Exception>();
            return this;
        }

        internal Mock<IImportRepository> MockFileImportData(List<FileImportDTO> result)
        {
            Setup(x => x.GetFileImportData(It.IsAny<string>()))
             .Returns(result);
            return this;
        }

        internal Mock<IImportRepository> MockFileImportDataException()
        {
            Setup(x => x.GetFileImportData(It.IsAny<string>()))
         .Throws<Exception>();
            return this;
        }

        internal Mock<IImportRepository> MockImportIsProcessedUpdate(FileImport fileImport)
        {
            Setup(x => x.GetFileImportDataByID(It.IsAny<int>()))
            .Returns(Task.FromResult(fileImport));
            Setup(x => x.UpdateFileImport(It.IsAny<FileImport>()))
             .Returns(fileImport);
            return this;
        }

        internal Mock<IImportRepository> MockImportIsProcessedUpdateException()
        {
            Setup(x => x.GetFileImportDataByID(It.IsAny<int>()))
        .Throws<Exception>();
            return this;
        }

        internal Mock<IImportRepository> MockGetAllImportTypes(List<ImportType> mockFileImport)
        {
            Setup(x => x.GetAllImportTypes(It.IsAny<long>()))
             .Returns(mockFileImport);
            return this;
        }

        internal Mock<IImportRepository> MockGetAllImportTypesException()
        {
            Setup(x => x.GetAllImportTypes(It.IsAny<long>()))
                .Throws<Exception>();
            return this;
        }

        internal Mock<IImportRepository> MockGetAllQuestionnaireItems(List<AssessmemtTemplateDTO> lists, ImportType importType)
        {
            Setup(x => x.GetAllQuestionnaireItems(It.IsAny<int>()))
        .Returns(lists);
            Setup(x => x.GetImportTypeIDByName(It.IsAny<string>()))
     .Returns(Task.FromResult(importType));
            return this;
        }

        internal Mock<IImportRepository> MockGetAllQuestionnaireItemsExceptions()
        {
            Setup(x => x.GetAllQuestionnaireItems(It.IsAny<int>()))
     .Throws<Exception>();
            Setup(x => x.GetImportTypeIDByName(It.IsAny<string>()))
              .Throws<Exception>();
            return this;
        }
    }
}
