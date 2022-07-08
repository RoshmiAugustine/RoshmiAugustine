using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class SearchServiceUnitTest
    {
        private SearchService searchService;
        private Mock<ISearchService> mockSearchService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<ISearchRepository> mockSearchRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IPersonRepository> mockPersonRepository;
        public SearchServiceUnitTest()
        {
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockSearchService = new Mock<ISearchService>();
            this.mockSearchRepository = new Mock<ISearchRepository>();
            this.mockPersonRepository = new Mock<IPersonRepository>();
        }

        #region Get User Details

        long agencyID = 1;
        string searchKey = "bin";
        int pageNo = 1;
        int pageSize = 12;
        List<string> roles = new List<string>
                    {
                       PCISEnum.Roles.SuperAdmin
                    };
        int helperID = 1;

        [Fact]
        public void GetUpperpaneSearch_Success_ReturnsCorrectResult()
        {
            List<UpperpaneSearchDTO> mockUpperpaneSearch = (List<UpperpaneSearchDTO>)this.MockUpperpaneSearchResults();
            this.mockSearchRepository = new MockSearchRepository().MockUpperpaneSearchResults(mockUpperpaneSearch);

            InitialiseUserService();

            UpperpaneSearchKeyDTO mockUpperpaneSearchKeyDTO = new UpperpaneSearchKeyDTO()
            {
                searchKey = searchKey,
                pageNo = pageNo,
                pageSize = pageSize
            };
            var result = this.searchService.GetUpperpaneSearchResults(mockUpperpaneSearchKeyDTO, agencyID, roles, helperID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }



        [Fact]
        public void GetUpperpaneSearch_Success_ReturnsNoResult()
        {
            var mockUpperpaneSearch = new List<UpperpaneSearchDTO>();//GetMockUpperpaneSearch();
            this.mockSearchRepository = new MockSearchRepository().MockUpperpaneSearchResults(mockUpperpaneSearch);

            InitialiseUserService();

            UpperpaneSearchKeyDTO mockUpperpaneSearchKeyDTO = new UpperpaneSearchKeyDTO()
            {
                searchKey = searchKey,
                pageNo = pageNo,
                pageSize = pageSize
            };
            var result = this.searchService.GetUpperpaneSearchResults(mockUpperpaneSearchKeyDTO, agencyID, roles, helperID);
            Assert.Empty(result.UpperpaneSearchList);
        }

        [Fact]
        public void GetUpperpaneSearch_Failure_ExceptionResult()
        {
            this.mockSearchRepository = new MockSearchRepository().MockUpperpaneSearchException();

            InitialiseUserService();

            UpperpaneSearchKeyDTO mockUpperpaneSearchKeyDTO = new UpperpaneSearchKeyDTO()
            {
                searchKey = searchKey,
                pageNo = pageNo,
                pageSize = pageSize
            };
            Assert.ThrowsAny<Exception>(() => this.searchService.GetUpperpaneSearchResults(mockUpperpaneSearchKeyDTO, agencyID, roles, helperID));
        }
        #endregion

        private void InitialiseUserService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.searchService = new SearchService(this.mockSearchRepository.Object, null, null, localize.Object,this.mockPersonRepository.Object);
        }

        private object MockUpperpaneSearchResults()
        {
            return new List<UpperpaneSearchDTO>()
            {
                new UpperpaneSearchDTO()
                {
                    Name = "Gibin",
                    Type = "H",
                    Id = 1,
                    Index=new Guid(),
                    TotalCount=4
                },
                new UpperpaneSearchDTO()
                    {
                    Name = "Gibin 1",
                    Type = "H",
                    Id = 2,
                    Index=new Guid(),
                    TotalCount=4
                },
                new UpperpaneSearchDTO()
                    {
                     Name = "Gibin 2",
                    Type = "P",
                    Id = 2,
                    Index=new Guid(),
                    TotalCount=3
                },
                new UpperpaneSearchDTO()
                    {
                    Name = "Gibin 3",
                    Type = "P",
                    Id = 1,
                    Index=new Guid(),
                    TotalCount=3
                }
            };
        }
    }
}
