using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Api.Controllers;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Api.Services;
using Opeeka.PICS.UnitTests.Api.Common;
using System;
using System.Collections.Generic;
using Xunit;
namespace Opeeka.PICS.UnitTests.Api.Controllers
{
    public class AssessmentControllerUnitTest 
    {
        /// Initializes a new instance of the assessmentservice/> class.
        private Mock<IAssessmentService> mockassessmentService;


        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<AssessmentController>> mockLogger;

        private AssessmentController assessmentController;
         private MockUserIdentity mockUserIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentController"/> class.
        /// </summary>
        /// 

        public AssessmentControllerUnitTest()
        {
            this.mockLogger = new Mock<ILogger<AssessmentController>>();
            this.mockUserIdentity = new MockUserIdentity();

        }
        private void InitialiseAssessmentService()
        {
            this.assessmentController = new AssessmentController(this.mockLogger.Object, this.mockassessmentService.Object);
            this.assessmentController.ControllerContext = mockUserIdentity.MockGetClaimsIdentity();
        }

        [Fact]
        public void GetQuestions_Success_ReturnsCorrectResult()
        {
            var mockQuestions = GetMockQuestionsResponseDTO();
            this.mockassessmentService = new MockAssessmentService().MockGetQuestions(mockQuestions);

            InitialiseAssessmentService();
            int id = 1;
            var result = this.assessmentController.GetQuestions(id);
            Assert.NotNull(result.Value.Questions);
            Assert.IsType<ActionResult<QuestionsResponseDTO>>(result);
            Assert.Equal(result.Value.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetQuestions_Success_ReturnsnullResult()
        {
            var mockQuestions = GetMockQuestionsResponseDTO();
            mockQuestions.Questions = null;
            this.mockassessmentService = new MockAssessmentService().MockGetQuestions(mockQuestions);

            InitialiseAssessmentService();
            int id = 1;
            var result = this.assessmentController.GetQuestions(id);

            Assert.Null(result.Value.Questions);
                    }

        [Fact]
        public void GetQuestions_Success_Failure_ExceptionResult()
        {
            this.mockassessmentService = new MockAssessmentService().MockGetQuestionsException();
            InitialiseAssessmentService();

            int id = 0;
            var result = (ObjectResult)this.assessmentController.GetQuestions(id).Result;

            Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
            Assert.Equal("An error occurred retrieving Questions. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        }

        [Fact]
        public void GetAssessmentDetails_Success_ReturnsCorrectResult()
        {
            this.mockassessmentService = new MockAssessmentService().MockGetAssessmentDetails(GetMockGetAssessmentDetailsDTO());
            InitialiseAssessmentService();
                        
            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            DateTime? date = DateTime.Now;
            int pageNumber = 1;
            long totalCount = 10;
            var result = this.assessmentController.GetAssessmentDetails(PersonIndex, QuestionnaireID, 0, pageNumber, totalCount, date);


            Assert.NotNull(result);
            Assert.IsType<ActionResult<AssessmentDetailsResponseDTO>>(result);
    
      
        }

        [Fact]
        public void GetAssessmentDetails_Success_ReturnsnullResult()
        {
            this.mockassessmentService = new MockAssessmentService().MockGetAssessmentDetails(new AssessmentDetailsResponseDTO());
            InitialiseAssessmentService();

            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            DateTime? date = DateTime.Now;
            int pageNumber = 1;
            int totalCount = 10;
            var result = this.assessmentController.GetAssessmentDetails(PersonIndex, QuestionnaireID, 0, pageNumber, totalCount, date);
            Assert.Null(result.Value);
        }

        //        [Fact]
        //public void GetAssessmentDetails_Success_Failure_ExceptionResult()
        //{
        //    this.mockassessmentService = new MockAssessmentService().MockGetAssessmentDetailsException();
        //    InitialiseAssessmentService();

        //    Guid PersonIndex = new Guid();
        //    int QuestionnaireID = 1;
        //    DateTime? date = DateTime.Now;
        //    int pageNumber = 1;
        //    int totalCount = 10;
        //    var result = (ObjectResult)this.assessmentController.GetAssessmentDetails(PersonIndex, QuestionnaireID, date,pageNumber, totalCount).Result;

        //    Assert.Equal(PCISEnum.StatusCodes.HTMLCodeDetected, result.StatusCode);
        //    Assert.Equal("An error occurred retrieving Assessment Details. Please try again later or contact support.: Exception of type 'System.Exception' was thrown.", result.Value);
        //}
        
        private QuestionsResponseDTO GetMockQuestionsResponseDTO()
        {
            var QuestionsResponseDTO = new QuestionsResponseDTO
            {
                ResponseStatus = PCISEnum.StatusMessages.Success,
                ResponseStatusCode = PCISEnum.StatusCodes.Success,
                Questions = new QuestionsDTO
                {
                    QuestionnaireID = 1,
                    QuestionnaireName = "QA1",
                    Categories = @"string"
                }
            };
            return QuestionsResponseDTO;
        }

        private AssessmentDetailsResponseDTO GetMockGetAssessmentDetailsDTO()
        {
            return new AssessmentDetailsResponseDTO
            {
                ResponseStatus = PCISEnum.StatusMessages.Success,
                ResponseStatusCode = PCISEnum.StatusCodes.Success,
                AssessmentDetails = GetMockAssessmentDetails()
            };
        }
        private List<AssessmentDetailsDTO> GetMockAssessmentDetails()
        {
            return new List<AssessmentDetailsDTO>()
            {
                new AssessmentDetailsDTO()
                {
                    AssessmentID=1,
                    AssessmentReasonID=1,
                    AssessmentStatusID=1,
                    Date=DateTime.Now,
                    DaysInProgram=0,
                    PersonID=1,
                    PersonQuestionnaireID=1,
                    TimePeriod=""
                },
                new AssessmentDetailsDTO()
                {
                    AssessmentID=2,
                    AssessmentReasonID=1,
                    AssessmentStatusID=1,
                    Date=DateTime.Now,
                    DaysInProgram=0,
                    PersonID=1,
                    PersonQuestionnaireID=1,
                    TimePeriod=""
                }
            };
        }
    }

}
