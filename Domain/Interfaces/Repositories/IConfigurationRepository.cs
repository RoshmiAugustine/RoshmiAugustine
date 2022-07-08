using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IConfigurationRepository
    {
        /// <summary>
        /// GetConfigurations as a List. 
        /// </summary>
        /// <returns>List of ConfigurationParameterDTO.</returns>
        List<ConfigurationParameterDTO> GetConfigurationList(long agencyID = 0);

        /// <summary>
        /// GetConfiguration details By Name.
        /// </summary>
        /// <param name="Name">Configuration Parameter Name</param>
        /// <param name="agencyID">Optional.</param>
        /// <returns>ConfigurationParameterDTO.</returns>
        ConfigurationParameterDTO GetConfigurationByName(string name, long agencyID = 0);
    }
}
