using System;
using System.Collections.Generic;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockReportRepository : Mock<IReportRepository>
    {
        internal Mock<IReportRepository> MockGetItemReportData(List<ItemDetailsDTO> mockitemDetailsList)
        {
            Setup(x => x.GetItemReportData(It.IsAny<ReportInputDTO>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
           .Returns(mockitemDetailsList);
            return this;
        }

        internal Mock<IReportRepository> MockGetItemReportDataException()
        {
            Setup(x => x.GetItemReportData(It.IsAny<ReportInputDTO>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
               .Throws<Exception>();
            return this;
        }

        internal Mock<IReportRepository> MockGetStoryMapReportData(List<StoryMapDTO> mockitemDetailsList)
        {
            Setup(x => x.GetStoryMapReportData(It.IsAny<ReportInputDTO>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
           .Returns(mockitemDetailsList);
            return this;
        }

        internal Mock<IReportRepository> MockGetStoryMapReportDataException()
        {
            Setup(x => x.GetStoryMapReportData(It.IsAny<ReportInputDTO>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
               .Throws<Exception>();
            return this;
        }
        internal Mock<IReportRepository> MockGetPersonStrengthFamilyReportData(PersonStrengthReportDTO mockresult)
        {
            Setup(x => x.GetPersonStrengthFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
             .Returns(mockresult);
            return this;
        }
        internal Mock<IReportRepository> MockGetPersonStrengthFamilyReportDataException()
        {
            Setup(x => x.GetPersonStrengthFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
               .Throws<Exception>();
            return this;
        }
        internal Mock<IReportRepository> MockGetPersonNeedsFamilyReportData(PersonNeedsReportDTO mockresult)
        {
            Setup(x => x.GetPersonNeedsFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
             .Returns(mockresult);
            return this;
        }
        internal Mock<IReportRepository> MockGetPersonNeedsFamilyReportDataException()
        {
            Setup(x => x.GetPersonNeedsFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
               .Throws<Exception>();
            return this;
        }
        internal Mock<IReportRepository> MockGetSupportResourcesFamilyReportData(SupportResourceReportDTO mockresult)
        {
            Setup(x => x.GetSupportResourcesFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
             .Returns(mockresult);
            return this;
        }
        internal Mock<IReportRepository> MockGetSupportResourcesFamilyReportDataException()
        {
            Setup(x => x.GetSupportResourcesFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
               .Throws<Exception>();
            return this;
        }
        internal Mock<IReportRepository> MockGetSupportNeedsFamilyReportData(SupportNeedsReportDTO mockresult)
        {
            Setup(x => x.GetSupportNeedsFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
             .Returns(mockresult);
            return this;
        }
        internal Mock<IReportRepository> MockGetSupportNeedsFamilyReportDataException()
        {
            Setup(x => x.GetSupportNeedsFamilyReportData(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
               .Throws<Exception>();
            return this;
        }

        internal Mock<IReportRepository> MockGetFamilyReportStatus(FamilyReportStatusDTO mockresult)
        {
            Setup(x => x.GetFamilyReportStatus(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
             .Returns(mockresult);
            return this;
        }
        internal Mock<IReportRepository> MockGetFamilyReportStatusException()
        {
            Setup(x => x.GetFamilyReportStatus(It.IsAny<long>(), It.IsAny<FamilyReportInputDTO>(), It.IsAny<List<int>>()))
               .Throws<Exception>();
            return this;
        }
    }
}
