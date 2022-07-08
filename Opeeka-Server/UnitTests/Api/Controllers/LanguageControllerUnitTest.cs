using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Api.Common;
using Opeeka.PICS.UnitTests.Api.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opeeka.PICS.UnitTests.Api.Controllers
{
    public class LanguageControllerUnitTest
    {
        /// Initializes a new instance of the languageservice/> class.
        private Mock<ILanguageService> mocklanguageService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<LanguageController>> mockLogger;

        private LanguageController languageController;

        private MockUserIdentity mockUserIdentity;

        private MockResponse mockResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageController"/> class.
        /// </summary>
        /// 
        public LanguageControllerUnitTest()
        {
            this.mockLogger = new Mock<ILogger<LanguageController>>();
            this.mockUserIdentity = new MockUserIdentity();
            this.mockResponse = new MockResponse();

        }
        private void InitialiseAssessmentService()
        {
            this.languageController = new LanguageController(this.mockLogger.Object, this.mocklanguageService.Object);
            this.languageController.ControllerContext = mockUserIdentity.MockGetClaimsIdentity();
        }

        //--------------------------------------------------------AddLanguage--------------------------------------------------------
        [Fact]
        public void AddLanguage_Success_Result()
        {
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.InsertionSuccess, PCISEnum.StatusMessages.InsertionSuccess);
            this.mocklanguageService = new MockLanguageService().MockAddLanguage(cRUDResponseDTO);

            InitialiseAssessmentService();
            var mockLanguage = GetMockLanguageDetailsDTO();
            var result = this.languageController.AddLanguage(mockLanguage);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<CRUDResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        //--------------------------------------------------------AddLanguage--------------------------------------------------------
        [Fact]
        public void AddLanguage_ArgumentNullException_Result()
        {
            this.mocklanguageService = new MockLanguageService().MockAddLanguageArgumentNullException();

            InitialiseAssessmentService();

            Assert.Null(this.languageController.AddLanguage(null));
        }

        ////--------------------------------------------------------AddLanguage--------------------------------------------------------
        //[Fact]
        //public void AddLanguage_Exception_Result()
        //{
        //    this.mocklanguageService = new MockLanguageService().MockAddLanguageException();

        //    InitialiseAssessmentService();
        //    var mockLanguage = GetMockLanguageDetailsDTO();
        //    var result = (ObjectResult)this.languageController.AddLanguage(mockLanguage).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        ////--------------------------------------------------------AddLanguage--------------------------------------------------------
        [Fact]
        public void AddLanguage_ModelStateNotValid_Result()
        {
            this.mocklanguageService = new MockLanguageService().MockAddLanguage(new CRUDResponseDTO());

            InitialiseAssessmentService();
            var mockLanguage = new LanguageDetailsDTO();
            this.languageController.ModelState.AddModelError("Name", "Name is Required");
            var result = (EmptyResult)this.languageController.AddLanguage(mockLanguage).Result;

            Assert.IsType<EmptyResult>(result);
        }

        //--------------------------------------------------------UpdateLanguage--------------------------------------------------------
        [Fact]
        public void UpdateLanguage_Success_Result()
        {
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.UpdationSuccess, PCISEnum.StatusMessages.UpdationSuccess);
            this.mocklanguageService = new MockLanguageService().MockUpdateLanguage(cRUDResponseDTO);

            InitialiseAssessmentService();
            var mockLanguage = GetMockLanguageDetailsDTO();
            var result = this.languageController.UpdateLanguage(mockLanguage);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<CRUDResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.UpdationSuccess);
        }

        ////--------------------------------------------------------UpdateLanguage--------------------------------------------------------
        //[Fact]
        //public void UpdateLanguage_Exception_Result()
        //{
        //    this.mocklanguageService = new MockLanguageService().MockUpdateLanguageException();

        //    InitialiseAssessmentService();
        //    var mockLanguage = GetMockLanguageDetailsDTO();
        //    var result = (ObjectResult)this.languageController.UpdateLanguage(mockLanguage).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        ////--------------------------------------------------------UpdateLanguage--------------------------------------------------------
        [Fact]
        public void UpdateLanguage_ModelStateNotValid_Result()
        {
            this.mocklanguageService = new MockLanguageService().MockUpdateLanguage(new CRUDResponseDTO());

            InitialiseAssessmentService();
            var mockLanguage = new LanguageDetailsDTO();
            this.languageController.ModelState.AddModelError("Name", "Name is Required");
            var result = (EmptyResult)this.languageController.UpdateLanguage(mockLanguage).Result;

            Assert.IsType<EmptyResult>(result);
        }

        ////-------------------------------------------------------- DeleteLanguage-----------------------------------------------
        [Fact]
        public void DeleteLanguage_Success_Result()
        {
            var cRUDResponseDTO = this.mockResponse.GetMockResponseDTO(PCISEnum.StatusCodes.DeletionSuccess, PCISEnum.StatusMessages.DeletionSuccess);
            this.mocklanguageService = new MockLanguageService().MockDeleteLanguage(cRUDResponseDTO);

            InitialiseAssessmentService();
            var languageId = 1;
            var result = this.languageController.DeleteLanguage(languageId);

            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.DeletionSuccess);
        }

        ////-------------------------------------------------------- DeleteLanguage-----------------------------------------------
        //[Fact]
        //public void DeleteLanguage_Exception_Result()
        //{
        //    this.mocklanguageService = new MockLanguageService().MockDeleteLanguageException();

        //    InitialiseAssessmentService();
        //    var languageId = 1;
        //    var result = (ObjectResult)this.languageController.DeleteLanguage(languageId).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}

        ////-------------------------------------------------------- GetLanguageList-----------------------------------------------
        [Fact]
        public void GetLanguageList_Success_Result()
        {
            this.mocklanguageService = new MockLanguageService().MockGetLanguageList(GetMockLanguageListResponseDTO());

            InitialiseAssessmentService();
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.languageController.GetLanguageList(pageNumber, pageSize);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<LanguageListResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }


        ////-------------------------------------------------------- GetLanguageList-----------------------------------------------
        //[Fact]
        //public void GetLanguageList_Exception_Result()
        //{
        //    this.mocklanguageService = new MockLanguageService().MockGetLanguageListException();

        //    int pageNumber = 1;
        //    int pageSize = 10;
        //    InitialiseAssessmentService();
        //    var result = (ObjectResult)this.languageController.GetLanguageList(pageNumber, pageSize).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving case note types. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}
        private LanguageListResponseDTO GetMockLanguageListResponseDTO()
        {
            return new LanguageListResponseDTO
            {
                ResponseStatus = PCISEnum.StatusMessages.Success,
                ResponseStatusCode = PCISEnum.StatusCodes.Success,
                LanguageList = GetMockLanguageDTO()

            };
        }
        private List<LanguageDTO> GetMockLanguageDTO()
        {
            var listLanguageDTO = new List<LanguageDTO>();
            var languageDTO1 = new LanguageDTO
            {
                LanguageID = 1,
                Abbrev = "abbrev1",
                Description = "Description",
                ListOrder = 2,
                UpdateUserID = 5,
                AgencyID = 100,
                IsRemoved = false,
                UpdateDate = DateTime.Now

            };
            listLanguageDTO.Add(languageDTO1);
            var languageDTO2 = new LanguageDTO
            {
                LanguageID = 1,
                Abbrev = "abbrev1",
                Description = "Description",
                ListOrder = 2,
                UpdateUserID = 5,
                AgencyID = 100,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            listLanguageDTO.Add(languageDTO2);
            return listLanguageDTO;
        }
        private LanguageDetailsDTO GetMockLanguageDetailsDTO()
        {
            return new LanguageDetailsDTO
            {
                LanguageID = 1,
                Abbrev = "abbrev1",
                Description = "Description",
                ListOrder = 2,
                UpdateUserID = 5,
                AgencyID = 100
            };
        }
    }
}
