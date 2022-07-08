using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAgencySharingHistoryRepository
    {
        AgencySharingHistoryDTO AddAgencySharingHistroy(AgencySharingHistoryDTO agencySharingHistoryDTO);

    }
}
