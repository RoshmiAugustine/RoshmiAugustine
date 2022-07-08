using Moq;
using Opeeka.PICS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.UnitTests.Api.Common
{
    public class MockResponse
    {
        private readonly CRUDResponseDTO cRUDResponseDTO;

        public MockResponse()
        {
            this.cRUDResponseDTO = new CRUDResponseDTO();
        }
        public CRUDResponseDTO GetMockResponseDTO(int statusCode, string statusMessages)
        {
             this.cRUDResponseDTO.ResponseStatusCode = statusCode;
            this.cRUDResponseDTO.ResponseStatus = statusMessages;
            this.cRUDResponseDTO.Id = 0;
            return cRUDResponseDTO;
        }
    }
}
