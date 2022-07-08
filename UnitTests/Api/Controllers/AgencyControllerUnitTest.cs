using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Api.Controllers;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Api.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Opeeka.PICS.UnitTests.Api.Common;

namespace Opeeka.PICS.UnitTests.Api.Controllers
{
    public class AgencyControllerUnitTest
    {
        /// Initializes a new instance of the agencyService/> class.
        private Mock<IAgencyService> mockagencyService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<AgencyController>> mockLogger;

        private AgencyController agencyController;

        private MockResponse mockResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyController"/> class.
        /// </summary>
        /// 
        public AgencyControllerUnitTest()
        {
            this.mockLogger = new Mock<ILogger<AgencyController>>();
            this.mockResponse = new MockResponse();
        }
        private void InitialiseAgencyService()
        {
            this.agencyController = new AgencyController(this.mockLogger.Object, this.mockagencyService.Object);
        }
        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_Success_Result()
        {
            var mockAgency = GetMockAgencyDetails();
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.InsertionSuccess, PCISEnum.StatusMessages.InsertionSuccess);
            this.mockagencyService = new MockAgencyService().MockAddAgency(cRUDResponseDTO, mockAgency);

            InitialiseAgencyService();
            var result = this.agencyController.AddAgencyDetails(mockAgency);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<CRUDResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_Failed_Result()
        {
            var mockAgency = GetMockAgencyDetails();
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.insertionFailed, PCISEnum.StatusMessages.insertionFailed);
            this.mockagencyService = new MockAgencyService().MockAddAgency(cRUDResponseDTO, mockAgency);

            InitialiseAgencyService();
            var result = this.agencyController.AddAgencyDetails(mockAgency);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.insertionFailed);
        }

        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_InvalidAbbreviation_Result()
        {
            var mockAgency = GetMockAgencyDetails();
            mockAgency.Abbrev = null;
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.InvalidAbbrev, PCISEnum.StatusMessages.InvalidAbbrev);
            this.mockagencyService = new MockAgencyService().MockAddAgency(cRUDResponseDTO, mockAgency);

            InitialiseAgencyService();
            var result = this.agencyController.AddAgencyDetails(mockAgency);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.InvalidAbbrev);
        }

        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_InvalidAgencyName_Result()
        {
            var mockAgency = GetMockAgencyDetails();
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.InvalidName, PCISEnum.StatusMessages.InvalidName);
            mockAgency.AgencyID = 0;
            this.mockagencyService = new MockAgencyService().MockAddAgency(cRUDResponseDTO, mockAgency);

            InitialiseAgencyService();
            var result = this.agencyController.AddAgencyDetails(mockAgency);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.InvalidName);
        }

        ////--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void AddAgencyDetails_ArgumentNullException_Result()
        {
            var mockAgency = new AgencyDetailsDTO();
            mockAgency = null;
            this.mockagencyService = new MockAgencyService().MockAddAgencyArgumentNullException();

            InitialiseAgencyService();
            Assert.ThrowsAny<ArgumentNullException>(() => this.agencyController.AddAgencyDetails(mockAgency));
        }

        ////--------------------------------------------------------Add Agency--------------------------------------------------------
        //[Fact]
        //public void AddAgencyDetails_Exception_Result()
        //{
        //    var mockAgency = new AgencyDetailsDTO();
        //    this.mockagencyService = new MockAgencyService().MockAddAgencyException();

        //    InitialiseAgencyService();
        //    var result = (ObjectResult)this.agencyController.AddAgencyDetails(mockAgency).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        //--------------------------------------------------------edit Agency--------------------------------------------------------
        [Fact]
        public void UpdateAgencyDetails_Success_Result()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.UpdationSuccess, PCISEnum.StatusMessages.UpdationSuccess);
            this.mockagencyService = new MockAgencyService().MockUpdateAgency(cRUDResponseDTO, mockAgencyDetails);

            InitialiseAgencyService();
            var result = this.agencyController.UpdateAgencyDetails(mockAgencyDetails);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<CRUDResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.UpdationSuccess);
        }

        //--------------------------------------------------------edit Agency--------------------------------------------------------
        [Fact]
        public void UpdateAgencyDetails_Failed_Result()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.UpdationFailed, PCISEnum.StatusMessages.UpdationFailed);
            this.mockagencyService = new MockAgencyService().MockUpdateAgency(cRUDResponseDTO, mockAgencyDetails);

            InitialiseAgencyService();
            var result = this.agencyController.UpdateAgencyDetails(mockAgencyDetails);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.UpdationFailed);
        }

        //--------------------------------------------------------edit Agency--------------------------------------------------------
        [Fact]
        public void UpdateAgencyDetails_InvalidAbbreviation_Result()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            mockAgencyDetails.Abbrev = null;
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.InvalidAbbrev, PCISEnum.StatusMessages.InvalidAbbrev);
            this.mockagencyService = new MockAgencyService().MockUpdateAgency(cRUDResponseDTO, mockAgencyDetails);

            InitialiseAgencyService();
            var result = this.agencyController.UpdateAgencyDetails(mockAgencyDetails);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.InvalidAbbrev);
        }
        //--------------------------------------------------------edit Agency--------------------------------------------------------

        [Fact]
        public void UpdateAgencyDetails_InvalidAgency_Result()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            mockAgencyDetails.AgencyID = 0;
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.InvalidName, PCISEnum.StatusMessages.InvalidName);
            this.mockagencyService = new MockAgencyService().MockUpdateAgency(cRUDResponseDTO, mockAgencyDetails);

            InitialiseAgencyService();
            var result = this.agencyController.UpdateAgencyDetails(mockAgencyDetails);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.InvalidName);
        }

        //--------------------------------------------------------edit Agency--------------------------------------------------------

        [Fact]
        public void UpdateAgencyDetails_RequiredParameterMissing_Result()
        {
            var mockAgencyDetails = GetMockAgencyDetails();
            mockAgencyDetails.AgencyIndex = Guid.Empty;
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.RequiredParameterMissing, PCISEnum.StatusMessages.RequiredParameterMissing);
            this.mockagencyService = new MockAgencyService().MockUpdateAgency(cRUDResponseDTO, mockAgencyDetails);

            InitialiseAgencyService();
            var result = this.agencyController.UpdateAgencyDetails(mockAgencyDetails);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.RequiredParameterMissing);
        }

        ////--------------------------------------------------------edit Agency--------------------------------------------------------
        [Fact]
        public void UpdateAgencyDetails_ArgumentNullException_Result()
        {
            var mockAgency = new AgencyDetailsDTO();
            mockAgency = null;
            this.mockagencyService = new MockAgencyService().MockUpdateAgencyArgumentNullException();

            InitialiseAgencyService();
            Assert.ThrowsAny<ArgumentNullException>(() => this.agencyController.UpdateAgencyDetails(mockAgency));
        }

        ////--------------------------------------------------------edit Agency--------------------------------------------------------
        //[Fact]
        //public void UpdateAgencyDetails_Exception_Result()
        //{
        //    var mockAgency = new AgencyDetailsDTO();
        //    this.mockagencyService = new MockAgencyService().MockUpdateAgencyException();

        //    InitialiseAgencyService();
        //    var result = (ObjectResult)this.agencyController.UpdateAgencyDetails(mockAgency).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        ////--------------------------------------------------------GetAgencyDetails-------------------------------------------------
        [Fact]
        public void GetAgencyDetailss_Success_ReturnsCorrectResult()
        {
            var mockAgencyDetails = GetMockAgencyDetailsDTO();
            this.mockagencyService = new MockAgencyService().MockGetAgencyDetails(mockAgencyDetails);

            InitialiseAgencyService();
            Guid AgencyIndex = new Guid();
            var result = this.agencyController.GetAgencyDetails(AgencyIndex);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<GetAgencyDetailsDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        ////--------------------------------------------------------GetAgencyDetails-------------------------------------------------
        [Fact]
        public void GetAgencyDetailss_Success_ReturnsNoResult()
        {
            var mockAgencyDetails = GetMockAgencyDetailsDTO();
            mockAgencyDetails.AgencyData = null;
            this.mockagencyService = new MockAgencyService().MockGetAgencyDetails(mockAgencyDetails);

            InitialiseAgencyService();
            Guid agencyIndex = new Guid();
            var result = this.agencyController.GetAgencyDetails(agencyIndex);

            Assert.Null(result.Value.AgencyData);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        ////--------------------------------------------------------GetAgencyDetails-------------------------------------------------
        //[Fact]
        //public void GetAgencyDetails_Exception_Result()
        //{
        //    this.mockagencyService = new MockAgencyService().MockGetAgencyDetailsException();

        //    InitialiseAgencyService();
        //    Guid agencyIndex = new Guid();
        //    var result = (ObjectResult)this.agencyController.GetAgencyDetails(agencyIndex).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        ////-------------------------------------------------------- RemoveAgencyDetails-----------------------------------------------
        [Fact]
        public void RemoveAgencyDetails_Success_Result()
        {
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.DeletionSuccess, PCISEnum.StatusMessages.DeletionSuccess);
            this.mockagencyService = new MockAgencyService().RemoveAgencyDetails(cRUDResponseDTO);

            InitialiseAgencyService();
            Guid agencyIndex = new Guid();
            var result = this.agencyController.RemoveAgencyDetails(agencyIndex);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.DeletionSuccess);
        }

        ////-------------------------------------------------------- RemoveAgencyDetails-----------------------------------------------
        [Fact]
        public void RemoveAgencyDetails_Failed_Result()
        {
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.DeletionFailed, PCISEnum.StatusMessages.DeletionFailed);
            this.mockagencyService = new MockAgencyService().RemoveAgencyDetails(cRUDResponseDTO);

            InitialiseAgencyService();
            Guid agencyIndex = new Guid();
            var result = this.agencyController.RemoveAgencyDetails(agencyIndex);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.DeletionFailed);
        }

        ////-------------------------------------------------------- RemoveAgencyDetails-----------------------------------------------
        //[Fact]
        //public void RemoveAgencyDetails_Exception()
        //{
        //    this.mockagencyService = new MockAgencyService().MockRemoveAgencyDetailsException();

        //    InitialiseAgencyService();
        //    Guid agencyIndex = Guid.Empty;
        //    var result = (ObjectResult)this.agencyController.RemoveAgencyDetails(agencyIndex).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}


        ////--------------------------------------------------------GetAgencyDetails-------------------------------------------------
        [Fact]
        public void GetAllAgencies_Success_Result()
        {
            var mockAgencyLookupResponse = GetMockAgencyLookupResponseDTO();
            this.mockagencyService = new MockAgencyService().MockGetAllAgencies(mockAgencyLookupResponse);

            InitialiseAgencyService();
            var result = this.agencyController.GetAllAgencies();

            Assert.NotNull(result);
            Assert.IsType<ActionResult<AgencyLookupResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        ////--------------------------------------------------------GetAgencyDetails-------------------------------------------------
        //[Fact]
        //public void GetAllAgencies_Exception_Result()
        //{
        //    this.mockagencyService = new MockAgencyService().MockGetAllAgenciesException();

        //    InitialiseAgencyService();
        //    var result = (ObjectResult)this.agencyController.GetAllAgencies().Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving all agency. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        private AgencyLookupResponseDTO GetMockAgencyLookupResponseDTO()
        {
            return new AgencyLookupResponseDTO
            {
                ResponseStatus = PCISEnum.StatusMessages.Success,
                ResponseStatusCode = PCISEnum.StatusCodes.Success,
                AgencyLookup = GetMockAgencyLookupDTO()
            };
        }
        private List<AgencyLookupDTO> GetMockAgencyLookupDTO()
        {
            return new List<AgencyLookupDTO>()
            {
                new AgencyLookupDTO
                {
                    Name = "Test1",
                    AgencyID = 1,
                    AgencyIndex = Guid.NewGuid(),
                    Abbrev="abbrev"
                },
                new AgencyLookupDTO
                {
                        Name = "Test2",
                    AgencyID = 2,
                    AgencyIndex = Guid.NewGuid(),
                    Abbrev="abbrev1"
                }
            };
        }

        private GetAgencyDetailsDTO GetMockAgencyDetailsDTO()
        {
            return new GetAgencyDetailsDTO
            {
                ResponseStatus = PCISEnum.StatusMessages.Success,
                ResponseStatusCode = PCISEnum.StatusCodes.Success,
                AgencyData = GetAgencyDataDTO()
            };
        }
        private AgencyDataDTO GetAgencyDataDTO()
        {
            return new AgencyDataDTO
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
