using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface ICollaborationSharingHistoryRepository
    {
        CollaborationSharingHistoryDTO AddCollaborationSharingHistroy(CollaborationSharingHistoryDTO collaborationHistoryDTO);
    }
}
