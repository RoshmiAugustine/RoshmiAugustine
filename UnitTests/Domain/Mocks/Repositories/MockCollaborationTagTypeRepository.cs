using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockCollaborationTagTypeRepository : Mock<ICollaborationTagTypeRepository>
    {
        public MockCollaborationTagTypeRepository MockAddCollaborationTagType(CollaborationTagTypeDTO result)
        {
            Setup(x => x.AddCollaborationTagType(It.IsAny<CollaborationTagTypeDTO>()))
                .Returns(result);
            return this;
        }

        public MockCollaborationTagTypeRepository MockUpdateCollaborationTagType(CollaborationTagTypeDTO result)
        {
            Setup(x => x.UpdateCollaborationTagType(It.IsAny<CollaborationTagTypeDTO>()))
                .Returns(result);
            return this;
        }

        public MockCollaborationTagTypeRepository MockGetCollaborationTagTypeList(List<CollaborationTagTypeDTO> result, long agencyId = 0)
        {
            if (agencyId == 0)
            {
                Setup(x => x.GetCollaborationTagTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                    .Returns(result);
                return this;
            }
            else
            {
                Setup(x => x.GetCollaborationTagTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(result);
                return this;
            }
        }

        public MockCollaborationTagTypeRepository MockGetCollaborationTagType(CollaborationTagTypeDTO result)
        {
            Setup(x => x.GetCollaborationTagType(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }

        public MockCollaborationTagTypeRepository MockGetCollaborationTagTypeCount()
        {
            Setup(x => x.GetCollaborationTagTypeCount(1))
                .Returns(0);
            return this;
        }

        public MockCollaborationTagTypeRepository MockAddCollaborationTagTypeFailure(CollaborationTagTypeDTO result)
        {
            Setup(x => x.AddCollaborationTagType(It.IsAny<CollaborationTagTypeDTO>()))
                .Returns(new CollaborationTagTypeDTO() { CollaborationTagTypeID = 0 });
            return this;
        }

        public MockCollaborationTagTypeRepository MockUpdateCollaborationTagTypeFailure(CollaborationTagTypeDTO result)
        {
            Setup(x => x.UpdateCollaborationTagType(It.IsAny<CollaborationTagTypeDTO>()))
                .Returns((CollaborationTagTypeDTO)null);
            return this;
        }

        public MockCollaborationTagTypeRepository MockGetCollaborationTagTypeException(CollaborationTagTypeDTO result)
        {
            Setup(x => x.GetCollaborationTagType(It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

        public MockCollaborationTagTypeRepository MockCollaborationTagTypeListException(List<CollaborationTagTypeDTO> results, long agencyId = 0)
        {
            if (agencyId == 0)
            {
                Setup(x => x.GetCollaborationTagTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
                Setup(x => x.GetCollaborationTagTypeCount(1))
                    .Returns(results.Count);
                return this;
            }
            else
            {
                Setup(x => x.GetCollaborationTagTypeList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
                Setup(x => x.GetCollaborationTagTypeCount(1))
                    .Returns(results.Count);
                return this;
            }
        }

        public MockCollaborationTagTypeRepository MockAddCollaborationTagTypeException(CollaborationTagTypeDTO result)
        {
            Setup(x => x.AddCollaborationTagType(It.IsAny<CollaborationTagTypeDTO>()))
                .Throws<Exception>();
            return this;
        }

        public MockCollaborationTagTypeRepository MockUpdateCollaborationTagTypeException(CollaborationTagTypeDTO result)
        {
            Setup(x => x.UpdateCollaborationTagType(It.IsAny<CollaborationTagTypeDTO>()))
                .Throws<Exception>();
            return this;
        }
    }
}
