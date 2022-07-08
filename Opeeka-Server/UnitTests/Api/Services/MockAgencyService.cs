
using Microsoft.AspNetCore.Mvc;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using System;


namespace Opeeka.PICS.UnitTests.Api.Services
{
    public class MockAgencyService : Mock<IAgencyService>
    {


        public MockAgencyService MockAddAgency(CRUDResponseDTO result, AgencyDetailsDTO agencyDetails)
        {

            Setup(x => x.AddAgencyDetails(It.IsAny<AgencyDetailsDTO>()))
                .Returns(result);
            return this;

        }

        public MockAgencyService MockAddAgencyArgumentNullException()
        {
            Setup(x => x.AddAgencyDetails(It.IsAny<AgencyDetailsDTO>()))
                   .Throws<ArgumentNullException>();
            return this;
        }

        public MockAgencyService MockAddAgencyException()
        {
            Setup(x => x.AddAgencyDetails(It.IsAny<AgencyDetailsDTO>()))
                .Throws<Exception>();
            return this;
        }

        public MockAgencyService MockUpdateAgency(CRUDResponseDTO result, AgencyDetailsDTO agencyDetails)
        {
            Setup(x => x.UpdateAgencyDetails(It.IsAny<AgencyDetailsDTO>()))
                .Returns(result);
            return this;
        }

        public MockAgencyService MockUpdateAgencyArgumentNullException()
        {
            Setup(x => x.UpdateAgencyDetails(It.IsAny<AgencyDetailsDTO>()))
                   .Throws<ArgumentNullException>();
            return this;
        }

        public MockAgencyService MockUpdateAgencyException()
        {
            Setup(x => x.UpdateAgencyDetails(It.IsAny<AgencyDetailsDTO>()))
                .Throws<Exception>();
            return this;
        }

        public MockAgencyService MockGetAgencyDetails(GetAgencyDetailsDTO agencyDetails)
        {
            Setup(x => x.GetAgencyDetails(It.IsAny<Guid>()))
                .Returns(agencyDetails);
            return this;
        }
        public MockAgencyService MockGetAgencyDetailsException()
        {
            Setup(x => x.GetAgencyDetails(It.IsAny<Guid>()))
                .Throws<Exception>();
            return this;
        }

        public MockAgencyService RemoveAgencyDetails(CRUDResponseDTO result)
        {
            Setup(x => x.RemoveAgencyDetails(It.IsAny<Guid>()))
                 .Returns(result);
            return this;
        }

        public MockAgencyService MockRemoveAgencyDetailsException()
        {
            Setup(x => x.RemoveAgencyDetails(It.IsAny<Guid>()))
     .Throws<Exception>();
            return this;
        }

        public MockAgencyService MockGetAllAgencies(AgencyLookupResponseDTO result)
        {
            Setup(x => x.GetAllAgencyLookup())
                .Returns(result);
            return this;
        }
        public MockAgencyService MockGetAllAgenciesException()
        {
            Setup(x => x.GetAllAgencyLookup())
                .Throws<Exception>();
            return this;
        }
    }
}
