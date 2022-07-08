using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockCollaborationLevelRepository : Mock<ICollaborationLevelRepository>
    {


        public MockCollaborationLevelRepository MockCollaborationLevelList(List<CollaborationLevelDTO> results)
        {
            Setup(x => x.GetCollaborationLevelList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetCollaborationLevelCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockCollaborationLevelRepository MockCollaborationLevelListException(List<CollaborationLevelDTO> results)
        {
            Setup(x => x.GetCollaborationLevelList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetCollaborationLevelCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockCollaborationLevelRepository MockAddCollaborationLevel(CollaborationLevelDTO result)
        {
            Setup(x => x.AddCollaborationLevel(It.IsAny<CollaborationLevelDTO>()))
                .Returns(result);
            return this;
        }

        public MockCollaborationLevelRepository MockAddCollaborationLevelException(CollaborationLevelDTO result)
        {
            Setup(x => x.AddCollaborationLevel(It.IsAny<CollaborationLevelDTO>()))
                 .Throws<Exception>();
            return this;
        }

        public MockCollaborationLevelRepository MockUpdateCollaborationLevel(CollaborationLevelDTO result)
        {
            Setup(x => x.UpdateCollaborationLevel(It.IsAny<CollaborationLevelDTO>()))
               .Returns(result);
            Setup(x => x.GetCollaborationLevel(It.IsAny<Int64>()))
                 .Returns(Task.FromResult(result));
            return this;
        }


        public MockCollaborationLevelRepository MockUpdateCollaborationLevelException(CollaborationLevelDTO result)
        {
            Setup(x => x.UpdateCollaborationLevel(It.IsAny<CollaborationLevelDTO>()))
                .Throws<Exception>();

            Setup(x => x.GetCollaborationLevel(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }

        public MockCollaborationLevelRepository MockdeleteCollaborationLevel(CollaborationLevelDTO result)
        {
            Setup(x => x.UpdateCollaborationLevel(It.IsAny<CollaborationLevelDTO>()))
               .Returns(result);

            Setup(x => x.GetCollaborationLevel(It.IsAny<Int64>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockCollaborationLevelRepository MockDeleteCollaborationLevelException(CollaborationLevelDTO result)
        {
            Setup(x => x.UpdateCollaborationLevel(It.IsAny<CollaborationLevelDTO>()))
                .Throws<Exception>();

            Setup(x => x.GetCollaborationLevel(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }
    }
}
