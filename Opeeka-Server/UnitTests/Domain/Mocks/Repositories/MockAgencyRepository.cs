
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAgencyRepository : Mock<IAgencyRepository>
    {
        public MockAgencyRepository MockGetByID(AgencyDTO result)
        {
            Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(result));

            return this;
        }

        public MockAgencyRepository MockAddAgency(Int64 result, AgencyDTO agencyResult)
        {
            Setup(x => x.AddAgency(It.IsAny<AgencyDTO>()))
                .Returns(result);
            Setup(x => x.GetAgencyDetailsByAbbrev(It.IsAny<string>()))
                .Returns(Task.FromResult(agencyResult));
            Setup(x => x.ValidateAgencyName(It.IsAny<AgencyDetailsDTO>()))
                 .Returns(Task.FromResult(true));
            Setup(x => x.ValidateAgencyAbbrev(It.IsAny<AgencyDetailsDTO>()))
                      .Returns(Task.FromResult(true));
            return this;
        }

        public MockAgencyRepository MockAddAgencyException()
        {
            Setup(x => x.AddAgency(It.IsAny<AgencyDTO>()))
                   .Throws<Exception>();
            Setup(x => x.GetAgencyDetailsByAbbrev(It.IsAny<string>()))
                .Throws<Exception>();
            Setup(x => x.ValidateAgencyName(It.IsAny<AgencyDetailsDTO>()))
                 .Throws<Exception>();
            Setup(x => x.ValidateAgencyAbbrev(It.IsAny<AgencyDetailsDTO>()))
                   .Throws<Exception>();
            return this;
        }

        public MockAgencyRepository MockUpdateAgency(AgencyDTO result, AgencyDTO agencyResult)
        {
            Setup(x => x.UpdateAgency(It.IsAny<AgencyDTO>()))
               .Returns(result);
            Setup(x => x.ValidateAgencyName(It.IsAny<AgencyDetailsDTO>()))
            .Returns(Task.FromResult(true));
            Setup(x => x.ValidateAgencyAbbrev(It.IsAny<AgencyDetailsDTO>()))
                      .Returns(Task.FromResult(true));
            Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(result));
            Setup(x => x.GetAgencyDetailsByAbbrev(It.IsAny<string>()))
                .Returns(Task.FromResult(agencyResult));
            return this;
        }

        public MockAgencyRepository MockUpdateAgencyException()
        {
            Setup(x => x.UpdateAgency(It.IsAny<AgencyDTO>()))
                  .Throws<Exception>();
            Setup(x => x.ValidateAgencyName(It.IsAny<AgencyDetailsDTO>()))
              .Throws<Exception>();
            Setup(x => x.ValidateAgencyAbbrev(It.IsAny<AgencyDetailsDTO>()))
                        .Throws<Exception>();
            Setup(x => x.GetAsync(It.IsAny<Guid>()))
                   .Throws<Exception>();
            Setup(x => x.GetAgencyDetailsByAbbrev(It.IsAny<string>()))
                   .Throws<Exception>();
            return this;
        }

        public MockAgencyRepository MockGetAgency(Agency result)
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<Agency, bool>>>()))
                  .Returns(Task.FromResult(result));
            return this;
        }
    }
}
