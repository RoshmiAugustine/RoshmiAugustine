using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockConfigurationRepository : Mock<IConfigurationRepository>
    {
        public MockConfigurationRepository GetConfigurationByName(ConfigurationParameterDTO result)
        {
            Setup(x => x.GetConfigurationByName(It.IsAny<string>(), It.IsAny<long>()))
                .Returns(result);
            return this;
        }
        public MockConfigurationRepository GetConfigurationByNameException()
        {
            Setup(x => x.GetConfigurationByName(It.IsAny<string>(), It.IsAny<long>()))
                .Throws<Exception>();
            return this;
        }
        public MockConfigurationRepository GetConfigurationList(List<ConfigurationParameterDTO> result)
        {
            Setup(x => x.GetConfigurationList(It.IsAny<long>()))
                .Returns(result);
            return this;
        }
        public MockConfigurationRepository GetConfigurationListException()
        {
            Setup(x => x.GetConfigurationList(It.IsAny<long>()))
                .Throws<Exception>();
            return this;
        }
    }
}
