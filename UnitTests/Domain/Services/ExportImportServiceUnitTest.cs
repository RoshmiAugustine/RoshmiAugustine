// -----------------------------------------------------------------------
// <copyright file="ExportImportServiceUnitTest.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Common;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class ExportImportServiceUnitTest
    {
        private Mock<IMapper> mockMapper;
        private Mock<IExportImportRepository> mockexportImportRepository;
        private Mock<LocalizeService> mocklocalize;
        private Mock<IImportRepository> mockimportRepository;
        private Mock<ILookupRepository> mocklookupRepository;
        private Mock<IConfigurationRepository> mockconfigurationRepository;
        private Mock<IHelperRepository> mockhelperRepository;
        private Mock<IEmailSender> mockemailSender;
        private ExportImportService exportImportService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;


        public ExportImportServiceUnitTest()
        {
            this.mockexportImportRepository = new Mock<IExportImportRepository>();
            this.mockimportRepository = new Mock<IImportRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mocklocalize = new Mock<LocalizeService>();
            this.mocklookupRepository = new Mock<ILookupRepository>();
            this.mockconfigurationRepository = new Mock<IConfigurationRepository>();
            this.mockhelperRepository = new Mock<IHelperRepository>();
            this.mockemailSender = new Mock<IEmailSender>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        private void InitialiseExportImportService(Mock<LocalizeService> localize)
        {

            this.exportImportService = new ExportImportService(this.mockexportImportRepository.Object, this.mockimportRepository.Object, localize.Object, this.mockMapper.Object, this.mocklookupRepository.Object, this.mockconfigurationRepository.Object, this.mockhelperRepository.Object, this.mockemailSender.Object, this.mockConfigRepository.Object, this.httpContextAccessor.Object);
        }

        [Fact]
        public void GetExportTemplateData_Success_ReturnsCorrectResult()
        {
            ExportTemplateDTO exportTemplateDTO = new ExportTemplateDTO()
            {
                TemplateSourceText = "Select top 10 * from Person",
                ExportTemplateID = 11,
            };
            this.mockexportImportRepository = new MockExportImportRepository().MockExportTemplateData(exportTemplateDTO);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseExportImportService(localize);
            int ExportTemplateID = 1;
            long agencyID = 1;
            var result = this.exportImportService.GetExportTemplateData(ExportTemplateID, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetExportTemplateData_Failure_ExceptionResult()
        {
            this.mockexportImportRepository = new MockExportImportRepository().MockExportTemplateDataException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseExportImportService(localize);
            int ExportTemplateID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.exportImportService.GetExportTemplateData(ExportTemplateID, agencyID));
        }

        [Fact]
        public void GetExportTemplateList_Success_ReturnsCorrectResult()
        {
            List<ExportTemplateDTO> ExportTemplate = new List<ExportTemplateDTO>();
            ExportTemplateDTO exportTemplateDTO = new ExportTemplateDTO()
            {
                TemplateSourceText = "Select top 10 * from Person",
                ExportTemplateID = 11,
            };
            ExportTemplate.Add(exportTemplateDTO);
            this.mockexportImportRepository = new MockExportImportRepository().MockExportTemplateList(ExportTemplate);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseExportImportService(localize);
            long agencyID = 1;
            var result = this.exportImportService.GetExportTemplateList(agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void ImportFile_Success_ReturnsCorrectResult()
        {
            this.mockimportRepository = new MockImportRepository().MockImportFile(1, MockImportTypeList());
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.InitialiseExportImportService(localize);
            var result = this.exportImportService.ImportFile(AddMockFileImportInputDTO(), AddMockFileImportDTO());
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        [Fact]
        public void ImportFile_Failure_ExceptionResult()
        {
            this.mockimportRepository = new MockImportRepository().MockImportFileException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.insertionFailed);
            this.InitialiseExportImportService(localize);
            Assert.ThrowsAny<Exception>(() => this.exportImportService.ImportFile(AddMockFileImportInputDTO(), AddMockFileImportDTO()));
        }

        [Fact]
        public void GetFileImportData_Success_ReturnsCorrectResult()
        {
            string importType = "person";
            var mockFileImport = MockFileImportList();
            this.mockimportRepository = new MockImportRepository().MockFileImportData(mockFileImport);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseExportImportService(localize);
            var result = this.exportImportService.GetFileImportData(importType);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetFileImportData_ExceptionResult()
        {
            string importType = "person";
            this.mockimportRepository = new MockImportRepository().MockFileImportDataException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseExportImportService(localize);
            Assert.ThrowsAny<Exception>(() => this.exportImportService.GetFileImportData(importType));
        }

        [Fact]
        public void ImportIsProcessedUpdate_Success_ReturnsCorrectResult()
        {
            FileImport fileImport = AddMockFileImport();
            this.mockimportRepository = new MockImportRepository().MockImportIsProcessedUpdate(fileImport);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationSuccess);
            this.InitialiseExportImportService(localize);
            var result = this.exportImportService.ImportIsProcessedUpdate(1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.UpdationSuccess);
        }

        [Fact]
        public void ImportIsProcessedUpdate_Failure_ExceptionResult()
        {
            this.mockimportRepository = new MockImportRepository().MockImportIsProcessedUpdateException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationFailed);
            this.InitialiseExportImportService(localize);
            Assert.ThrowsAny<Exception>(() => this.exportImportService.ImportIsProcessedUpdate(1));
        }

        [Fact]
        public void GetAllImportTypes_Success_ReturnsCorrectResult()
        {
            var mockFileImport = MockFileImportList();
            this.mockimportRepository = new MockImportRepository().MockGetAllImportTypes(MockImportTypeList());
            this.mocklookupRepository = new MockLookupRepository().MockGetAllImportTypes(GetAllAgencyQuestionnaire());
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseExportImportService(localize);
            var result = this.exportImportService.GetAllImportTypes(1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllImportTypes_ExceptionResult()
        {
            this.mockimportRepository = new MockImportRepository().MockGetAllImportTypes(MockImportTypeList());
            this.mocklookupRepository = new MockLookupRepository().MockGetAllImportTypesException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseExportImportService(localize);
            Assert.ThrowsAny<Exception>(() => this.exportImportService.GetAllImportTypes(1));
        }

        [Fact]
        public void GetAssessmentTemplateFromQuestionnaireId_Success_ReturnsCorrectResult()
        {
         //   this.mockimportRepository = new MockImportRepository().MockGetImportTypeIDByName();
            this.mockimportRepository = new MockImportRepository().MockGetAllQuestionnaireItems(MockGetQuestionnaireItems(), MockImportType());
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseExportImportService(localize);
            var result = this.exportImportService.GetAssessmentTemplateFromQuestionnaireId(1,true);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAssessmentTemplateFromQuestionnaireId_ExceptionResult()
        {
            this.mockimportRepository = new MockImportRepository().MockGetAllQuestionnaireItemsExceptions();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseExportImportService(localize);
            Assert.ThrowsAny<Exception>(() => this.exportImportService.GetAssessmentTemplateFromQuestionnaireId(1,false));
        }

        //SendEmailAfterImport
        [Fact]
        public void SendEmailAfterImport_Success_ReturnsCorrectResult()
        {
            ImportEmailInputDTO importEmailInputDTO = mockImportEmailInputDTO();
            UserDetailsDTO userDetailsDTO = mockUSerDatailsDTO();
            var mockConfigParameter = MockConfigurationParameter();
            this.mockhelperRepository = new MockHelperRepository().MockUserDetails(userDetailsDTO);
            this.mockconfigurationRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            this.mockemailSender = new MockEmailSender().MockSendEmailAsync(HttpStatusCode.Accepted);
            FileImport fileImport = AddMockFileImport();
            this.mockimportRepository = new MockImportRepository().MockImportIsProcessedUpdate(fileImport);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.EmailSendSuccess);
            this.InitialiseExportImportService(localize);
            var result = this.exportImportService.SendEmailAfterImport(importEmailInputDTO);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendSuccess);
        }



        [Fact]
        public void SendEmailAfterImport_Failure_ExceptionResult()
        {
            ImportEmailInputDTO importEmailInputDTO = mockImportEmailInputDTO();
            UserDetailsDTO userDetailsDTO = mockUSerDatailsDTO();
            this.mockhelperRepository = new MockHelperRepository().MockUserDetailsException(userDetailsDTO);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.MissingEmailID);
            this.InitialiseExportImportService(localize);
            Assert.ThrowsAny<Exception>(() => this.exportImportService.SendEmailAfterImport(importEmailInputDTO));
        }

        #region MockData
        private FileImportDTO AddMockFileImportDTO()
        {
            FileImportDTO responseDTO = new FileImportDTO()
            {
                AgencyID = 1,
                ImportType = "Person",
                FileImportID = 1,
                IsProcessed = false,
                UpdateUserID = 1,
                FileJsonData = "",
                ImportTypeID = 1,
                QuestionnaireID = 1,
                CreatedDate = DateTime.Now
            };
            return responseDTO;
        }
        private FileImport AddMockFileImport()
        {
            FileImport response = new FileImport()
            {
                AgencyID = 1,
                FileImportID = 1,
                IsProcessed = true,
                UpdateUserID = 1,
                FileJsonData = "",
                ImportTypeID = 1,
                QuestionnaireID = 1,
                CreatedDate = DateTime.Now
            };
            return response;
        }
        private FileImportInputDTO AddMockFileImportInputDTO()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "FirstName,LastName,DateOfBirth,Email,Phone1,Phone1Code,Identified Gender,HelperEmail,HelperStartDate,CollaborationName,CollaborationStartDate,Race/Ethnicity1,Race/Ethnicity2,Race/Ethnicity3,Race/Ethnicity4,Race/Ethnicity5,IdentifierType1,IdentifierType2,IdentifierType3,IdentifierType4,IdentifierType5,IdentifierTypeID1,IdentifierTypeID2,IdentifierTypeID3,IdentifierTypeID4,IdentifierTypeID5,, \n FirstName,LastName,DateOfBirth,Email,Phone1,Phone1Code,Identified Gender,HelperEmail,HelperStartDate,CollaborationName,CollaborationStartDate,Race/Ethnicity1,Race/Ethnicity2,Race/Ethnicity3,Race/Ethnicity4,Race/Ethnicity5,IdentifierType1,IdentifierType2,IdentifierType3,IdentifierType4,IdentifierType5,IdentifierTypeID1,IdentifierTypeID2,IdentifierTypeID3,IdentifierTypeID4,IdentifierTypeID5"; 
           
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var file = fileMock.Object;

            FileImportInputDTO responseDTO = new FileImportInputDTO()
            {
                ImportTypeID = 2,
                QuestionnaireID = 1,
                UploadFile = file
            };
            return responseDTO;
        }
        private List<FileImportDTO> MockFileImportList()
        {
            return new List<FileImportDTO>()
            {
                new FileImportDTO()
                {
                    FileJsonData = "AF",
                    AgencyID = 1,
                    FileImportID = 1,
                    ImportType = "Person",
                    IsProcessed = false,
                    ImportTypeID = 2,
                    QuestionnaireID = 2,
                    CreatedDate = DateTime.Now,
                    UpdateUserID = 1
                }
            };
        }
        private List<QuestionnaireDTO> GetAllAgencyQuestionnaire()
        {
            return new List<QuestionnaireDTO>()
            {
                new QuestionnaireDTO()
                {
                    InstrumentName = "AF",
                    Description ="",
                    QuestionnaireID = 1,
                    InstrumentAbbrev = "Person",
                    UpdateDate = DateTime.Now,
                }
            };
        }
        private List<ImportType> MockImportTypeList()
        {
            return new List<ImportType>()
            {
                new ImportType()
                {
                    TemplateJson = "[{ \"FirstName\": \"\",\"LastName\": \"\",\"DateOfBirth\": \"\",\"Email\": \"\",\"Phone1\": \"\",\"Phone1Code\": \"\",\"Identified Gender\": \"\",\"HelperEmail\": \"\",\"HelperStartDate\": \"\", \"CollaborationName\": \"\", \"CollaborationStartDate\": \"\", \"Race/Ethnicity1\": \"\", \"Race/Ethnicity2\": \"\", \"Race/Ethnicity3\": \"\", \"Race/Ethnicity4\": \"\", \"Race/Ethnicity5\": \"\", \"IdentifierType1\": \"\", \"IdentifierType2\": \"\", \"IdentifierType3\": \"\", \"IdentifierType4\": \"\", \"IdentifierType5\": \"\", \"IdentifierTypeID1\": \"\", \"IdentifierTypeID2\": \"\", \"IdentifierTypeID3\": \"\", \"IdentifierTypeID4\": \"\", \"IdentifierTypeID5\": \"\" }]",
                    ListOrder = 1,
                    TemplateURL = "",
                    Name = "Person",
                    IsRemoved= false,
                    ImportTypeID = 2,
                    UpdateDate = DateTime.Now,
                }
            };
        }
        private ImportType MockImportType()
        {
            return new ImportType()
            {
                TemplateJson = "[{ \"FirstName\": \"\",\"LastName\": \"\",\"DateOfBirth\": \"\",\"Email\": \"\",\"Phone1\": \"\",\"Phone1Code\": \"\",\"Identified Gender\": \"\",\"HelperEmail\": \"\",\"HelperStartDate\": \"\", \"CollaborationName\": \"\", \"CollaborationStartDate\": \"\", \"Race/Ethnicity1\": \"\", \"Race/Ethnicity2\": \"\", \"Race/Ethnicity3\": \"\", \"Race/Ethnicity4\": \"\", \"Race/Ethnicity5\": \"\", \"IdentifierType1\": \"\", \"IdentifierType2\": \"\", \"IdentifierType3\": \"\", \"IdentifierType4\": \"\", \"IdentifierType5\": \"\", \"IdentifierTypeID1\": \"\", \"IdentifierTypeID2\": \"\", \"IdentifierTypeID3\": \"\", \"IdentifierTypeID4\": \"\", \"IdentifierTypeID5\": \"\" }]",
                ListOrder = 1,
                    TemplateURL = "",
                    Name = "Person",
                    IsRemoved= false,
                    ImportTypeID = 2,
                    UpdateDate = DateTime.Now,
            };
        }
        private List<AssessmemtTemplateDTO> MockGetQuestionnaireItems()
        {
            return new List<AssessmemtTemplateDTO>()
            {
                new AssessmemtTemplateDTO()
                {
                    QuestionnaireItemName = "QuestionnaireItemName",
                    CategoryFocus = "CategoryFocus",
                    DefaultItemvalue = "DefaultItemvalue",
                    QuestionnaireItemID =2
                }
            };
        }
        private ConfigurationParameterDTO MockConfigurationParameter()
        {
            ConfigurationParameterDTO mockConfigParameter = new ConfigurationParameterDTO()
            {
                ConfigurationParameterID = 1,
                Name = "Config1",
                Value = "https://www.sample.com/ExternalAssessment"
            };
            return mockConfigParameter;
        }

        private ImportEmailInputDTO mockImportEmailInputDTO()
        {
            return new ImportEmailInputDTO()
            {
                AgencyID = 1,
                HelperUserID = 13,
                ImportFileID = 2,
                ImportFileName = "Person.csv",
                IsProcessed = true,
                Message = "Successfuly Imported",
                RowNo = 0
            };
        }

        private UserDetailsDTO mockUSerDatailsDTO()
        {
            return new UserDetailsDTO()
            {
                HelperEmail = "gibin@naocoits.com",
                HelperID = 13,
                Name = "Gibin",
                Title = "Mr."
            };
        }
        #endregion MockData
    }
}
