using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockReportingUnitRepository : Mock<IReportingUnitRepository>
    {
        public MockReportingUnitRepository MockAddReportingUnit(ReportingUnitDTO result)
        {
            Setup(x => x.AddReportingUnit(It.IsAny<ReportingUnitDTO>()))
                .Returns(result);
            return this;
        }

        public MockReportingUnitRepository MockReportingUnitList(List<ReportingUnitDataDTO> results)
        {
            Setup(x => x.GetReportingUnitList(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(results);
            Setup(x => x.GetReportingUnitCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockReportingUnitRepository MockReportingUnitListException(List<ReportingUnitDataDTO> results)
        {
            Setup(x => x.GetReportingUnitList(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
               .Throws<Exception>();
            Setup(x => x.GetReportingUnitCount(It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }
        public MockReportingUnitRepository MockEditReportingUnit(ReportingUnit result)
        {
            Setup(x => x.UpdateReportingUnit(It.IsAny<ReportingUnit>()))
               .Returns(result);

            Setup(x => x.GetReportingUnit(It.IsAny<Guid>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockReportingUnitRepository MockPartnerAgencyList(List<PartnerAgencyDataDTO> results)
        {
            Setup(x => x.GetPartnerAgencyList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(results);
            return this;
        }

        public MockReportingUnitRepository MockPartnerAgencyListException(List<PartnerAgencyDataDTO> results)
        {
            Setup(x => x.GetPartnerAgencyList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
               .Throws<Exception>();
            return this;
        }

        public MockReportingUnitRepository MockRUCollaborationListException(List<RUCollaborationDataDTO> results)
        {
            Setup(x => x.GetRUCollaborationList(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
               .Throws<Exception>();
            return this;
        }
        public MockReportingUnitRepository MockRUCollaborationList(List<RUCollaborationDataDTO> results)
        {
            Setup(x => x.GetRUCollaborationList(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(results);
            return this;
        }
    }
}
