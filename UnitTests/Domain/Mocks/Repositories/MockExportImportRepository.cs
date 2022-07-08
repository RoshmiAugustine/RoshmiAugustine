using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockExportImportRepository: Mock<IExportImportRepository>
    {
        public Mock<IExportImportRepository> MockExportTemplateData(ExportTemplateDTO exportTemplateDTO)
        {
            object obj = new object();
            Setup(x => x.GetExportData(It.IsAny<string>()))
           .Returns(obj);

            Setup(x => x.GetAsync(It.IsAny<int>()))
               .Returns(Task.FromResult(exportTemplateDTO));
            return this;
        }

        public  Mock<IExportImportRepository> MockExportTemplateDataException()
        {
            Setup(x => x.GetAsync(It.IsAny<int>()))
               .Throws<Exception>();
            return this;
        }

        public Mock<IExportImportRepository> MockExportTemplateList(List<ExportTemplateDTO> exportTemplate)
        {
            Setup(x => x.GetExportTemplateList(It.IsAny<int>())).Returns(exportTemplate);
            return this;
        }

        internal Mock<IExportImportRepository> MockExportTemplateListException()
        {
            Setup(x => x.GetExportTemplateList(It.IsAny<int>()))
              .Throws<Exception>();
            return this;
            
        }
    }
}
