using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class AgencyServiceUnitTest
    {
        /// Initializes a new instance of the agencyRepository/> class.
        private Mock<IAgencyRepository> mockagencyRepository;



        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IAddressrepository> mockAddressRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<AgencyService>> mockLogger;

        /// Initializes a new instance of the agencyAddressRepository/> class.
        private Mock<IAgencyAddressRepository> mockAgencyAddressRepository;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private AgencyService agencyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyService"/> class.
        /// </summary>
        public AgencyServiceUnitTest()
        {
            this.mockAddressRepository = new Mock<IAddressrepository>();
            this.mockAgencyAddressRepository = new Mock<IAgencyAddressRepository>();
            this.mockLogger = new Mock<ILogger<AgencyService>>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        private void InitialiseAgencyService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.agencyService = new AgencyService(this.mockagencyRepository.Object, this.mockAddressRepository.Object,
                                                       this.mockAgencyAddressRepository.Object, this.mockLogger.Object,
                                                       localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, null);
        }

        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_Success_ReturnsCorrectResult()
        {
            var mockAgency = GetMockAgencyDetails();
            var mockAgencyDetails = GetMockAgency();
            mockAgencyDetails.AgencyID = 0;
            this.mockagencyRepository = new MockAgencyRepository().MockAddAgency(1, mockAgencyDetails);
            this.mockAddressRepository = new MockAddressRepository().MockAddAddress(1);
            this.mockAgencyAddressRepository = new MockAgencyAddressRepository().MockAddAgencyAddress(1);
            InitialiseAgencyService();
            var result = this.agencyService.AddAgencyDetails(mockAgency);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        ////--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_Failure_Exception()
        {
            var mockAgency = new AgencyDetailsDTO();
            var mockAgencyDetails = GetMockAgency();
            mockAgencyDetails.AgencyID = 0;
            this.mockagencyRepository = new MockAgencyRepository().MockAddAgencyException();
            this.mockAddressRepository = new MockAddressRepository().MockAddAddress(1);
            this.mockAgencyAddressRepository = new MockAgencyAddressRepository().MockAddAgencyAddress(1);
            InitialiseAgencyService();
            Assert.ThrowsAny<Exception>(() => this.agencyService.AddAgencyDetails(mockAgency));

        }
        //--------------------------------------------------------edit Agency--------------------------------------------------------
        [Fact]
        public void EditAgencyDetails_Success_ReturnsCorrectResult()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            var mockAgency = GetMockAgency();
            var mockaddress = GetMockAddressDTO();
            var mockgencyAddress = GetMockAgencyAddress();
            var mockgencyAddressDTO = GetMockAgencyAddressDTO();
            var mockAgencyDetailsUpdate = GetMockAgency();
            mockAgencyDetailsUpdate.AgencyID = 0;
            this.mockagencyRepository = new MockAgencyRepository().MockUpdateAgency(mockAgency, mockAgencyDetailsUpdate);
            this.mockAddressRepository = new MockAddressRepository().MockEditAddress(mockaddress);
            this.mockAgencyAddressRepository = new MockAgencyAddressRepository().MockUpdateAgencyAddress(mockgencyAddress, mockgencyAddressDTO);
            InitialiseAgencyService();
            var result = this.agencyService.UpdateAgencyDetails(mockAgencyDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.UpdationSuccess);
        }
        //--------------------------------------------------------edit Agency--------------------------------------------------------
        [Fact]
        public void EditAgencyDetails_Failure_Exception()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            var mockaddress = GetMockAddressDTO();
            var mockgencyAddress = GetMockAgencyAddress();
            var mockgencyAddressDTO = GetMockAgencyAddressDTO();
            var mockAgencyDetailsUpdate = GetMockAgency();
            mockAgencyDetailsUpdate.AgencyID = 0;
            this.mockagencyRepository = new MockAgencyRepository().MockUpdateAgencyException();
            this.mockAddressRepository = new MockAddressRepository().MockEditAddress(mockaddress);
            this.mockAgencyAddressRepository = new MockAgencyAddressRepository().MockUpdateAgencyAddress(mockgencyAddress, mockgencyAddressDTO);
            InitialiseAgencyService();
            Assert.ThrowsAny<Exception>(() => this.agencyService.UpdateAgencyDetails(mockAgencyDetails));
        }

        //[Fact]
        //public void GetAgencyList_Success_ReturnsCorrectResult()
        //{

        //    var mockAgencies = GetMockAgencies();
        //    this.mockagencyRepository = new MockAgencyRepository().MockAgencyList(mockAgencies);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
        //    this.agencyService = new AgencyService(this.mockagencyRepository.Object, this.mockAddressRepository.Object,
        //        this.mockAgencyAddressRepository.Object, this.mockLogger.Object,
        //        localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);

        //    int pageNumber = 1;
        //    int pageSize = 10;
        //    long agencyID = 1;
        //    List<string> roles = new List<string>();
        //    roles.Add("Super Admin");
        //    var result = this.agencyService.GetAgencyList(pageNumber, pageSize, agencyID, roles);
        //    Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        //    //Assert.NotNull
        //}

        //[Fact]
        //public void GetAgencyList_Success_ReturnsNoResult()
        //{
        //    var mockAgencies = new List<AgencyDTO>();
        //    this.mockagencyRepository = new MockAgencyRepository().MockAgencyList(mockAgencies);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
        //    this.agencyService = new AgencyService(this.mockagencyRepository.Object, this.mockAddressRepository.Object,
        //        this.mockAgencyAddressRepository.Object, this.mockLogger.Object,
        //        localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
        //    int pageNumber = 1;
        //    int pageSize = 10;
        //    long agencyID = 1;
        //    List<string> roles = new List<string>();
        //    roles.Add("Super Admin");
        //    var result = this.agencyService.GetAgencyList(pageNumber, pageSize, agencyID, roles);
        //    Assert.Equal(0, result.TotalCount);
        //    //Assert.NotNull
        //}

        //[Fact]
        //public void GetAgencyList_Failure_InvalidParameterResult()
        //{
        //    var mockAgencies = new List<AgencyDTO>();
        //    this.mockagencyRepository = new MockAgencyRepository().MockAgencyList(mockAgencies);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.RequiredParameterMissing);
        //    this.agencyService = new AgencyService(this.mockagencyRepository.Object, this.mockAddressRepository.Object,
        //        this.mockAgencyAddressRepository.Object, this.mockLogger.Object,
        //        localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
        //    int pageNumber = 0;
        //    int pageSize = 10;
        //    long agencyID = 1;
        //    List<string> roles = new List<string>();
        //    roles.Add("Super Admin");
        //    var result = this.agencyService.GetAgencyList(pageNumber, pageSize, agencyID, roles);
        //    Assert.Equal(0, result.TotalCount);
        //    //Assert.NotNull
        //}

        /**************for reference***********************/
        //[Fact]
        //public void GetAgencyList_Failure_ExceptionResult()
        //{
        //    List<AgencyDTO> mockAgencies = new List<AgencyDTO>();
        //    this.mockagencyRepository = new MockAgencyRepository().MockAgencyListException(mockAgencies);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.RequiredParameterMissing);
        //    this.agencyService = new AgencyService(this.mockagencyRepository.Object, this.mockAddressRepository.Object,
        //        this.mockAgencyAddressRepository.Object, this.mockLogger.Object,
        //        localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
        //    int pageNumber = 1;
        //    int pageSize = 10;
        //    long agencyID = 1;
        //    List<string> roles = new List<string>();
        //    roles.Add("Super Admin");
        //    Assert.ThrowsAny<Exception>(() => this.agencyService.GetAgencyList(pageNumber, pageSize, agencyID, roles));
        //    //Assert.NotNull
        //}
        /**************for reference***********************/
        /// <summary>
        /// The GetMockAgencies.
        /// </summary>
        /// <returns>The <see cref="List<AgencyDTO>"/>.</returns>
        private List<AgencyDTO> GetMockAgencies()
        {
            return new List<AgencyDTO>()
            {
                new AgencyDTO()
                {
                    Name = "Test1",
                    ContactFirstName = "ContactFirstName1",
                    ContactLastName = "ContactLastName2",
                    Phone1 = "Phone1",
                    Phone2 = "Phone2",
                    Email = "abc@xyz.com",
                    NumberOfAddresses = 1,
                    NumberOfCollaboration = 2,
                    AgencyID = 1,
                    AgencyIndex = Guid.NewGuid(),
                    Abbrev="abbrev"
                },
                new AgencyDTO()
                {
                    Name = "Test2",
                    ContactFirstName = "ContactFirstName1",
                    ContactLastName = "ContactLastName2",
                    Phone1 = "Phone1",
                    Phone2 = "Phone2",
                    Email = "abc@xyz.com",
                    NumberOfAddresses = 1,
                    NumberOfCollaboration = 2,
                    AgencyID = 1,
                    AgencyIndex = Guid.NewGuid(),
                    Abbrev="abbrev"
                },
                new AgencyDTO()
                {
                    Name = "Test3",
                    ContactFirstName = "ContactFirstName1",
                    ContactLastName = "ContactLastName2",
                    Phone1 = "Phone1",
                    Phone2 = "Phone2",
                    Email = "abc@xyz.com",
                    NumberOfAddresses = 1,
                    NumberOfCollaboration = 2,
                    AgencyID = 1,
                    AgencyIndex = Guid.NewGuid(),
                    Abbrev="abbrev"
                }
            };
        }
        /// <summary>
        /// The GetMockAgencyAddressDTO.
        /// </summary>
        /// <returns>The <see cref="AgencyAddressDTO"/>.</returns>
        private AgencyAddressDTO GetMockAgencyAddressDTO()
        {
            AgencyAddressDTO AgencyDTO = new AgencyAddressDTO()
            {
                AgencyAddressID = 2,
                AddressID = 1,
                AgencyID = 1,
                UpdateUserID = 1,
                IsPrimary = true,
            };
            return AgencyDTO;
        }
        /// <summary>
        /// The GetMockAgencyAddress.
        /// </summary>
        /// <returns>The <see cref="AgencyAddressDTO"/>.</returns>
        private AgencyAddress GetMockAgencyAddress()
        {
            AgencyAddress AgencyAddress = new AgencyAddress()
            {
                AgencyAddressID = 2,
                AddressID = 1,
                AgencyID = 1,
                UpdateUserID = 1,
                IsPrimary = true,
            };
            return AgencyAddress;
        }

        /// <summary>
        /// The GetMockAgencyDetails.
        /// </summary>
        /// <returns>The <see cref="AgencyDTO"/>.</returns>
        private AgencyDTO GetMockAgency()
        {
            AgencyDTO AgencyDTO = new AgencyDTO()
            {
                Name = "Test333",
                ContactFirstName = "ContactFirstName1",
                ContactLastName = "ContactLastName2",
                Phone1 = "Phone1",
                Phone2 = "Phone2",
                Email = "abc@xyz.com",
                NumberOfAddresses = 1,
                NumberOfCollaboration = 2,
                AgencyID = 1,
                AgencyIndex = Guid.NewGuid(),
                Abbrev = "abbrev12"
            };
            return AgencyDTO;
        }
        /// <summary>
        /// The GetMockPersonSupport.
        /// </summary>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private AddressDTO GetMockAddressDTO()
        {

            AddressDTO AddressDTO = new AddressDTO()

            {
                AddressIndex = new Guid("B6EA0154-95AD-4C4C-A198-25945A45A5F7"),
                AddressID = 1,
                Address1 = "asddf",
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                IsPrimary = true,
                CountryStateID = 1,
                Zip = "65677"
            };
            return AddressDTO;
        }
        private AgencyDetailsDTO GetMockAgencyDetails()
        {
            AgencyDetailsDTO agencyDetailsDTO = new AgencyDetailsDTO()
            {
                Name = "Test1",
                AgencyIndex = new Guid("63CB7DF3-B6F7-4937-AB3C-3F4443F28EAD"),
                AddressIndex = new Guid("B6EA0154-95AD-4C4C-A198-25945A45A5F7"),
                AgencyID = 1,
                AddressID = 1,
                UpdateUserID = 1,
                Note = "Test Notes",
                Abbrev = "Test Abbreviation",
                Phone1 = "1234567890",
                Phone2 = "9874563210",
                Email = "test@test.com",
                ContactFirstName = "First Name",
                ContactLastName = "Last Name",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Zip = "52001",
                Zip4 = "53001",
                CountryStateID = 1,
                City = "City"
            };
            return agencyDetailsDTO;
        }
    }
}
