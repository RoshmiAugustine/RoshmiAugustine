// -----------------------------------------------------------------------
// <copyright file="MockCollaborationSharingRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockCollaborationSharingRepository : Mock<ICollaborationSharingRepository>
    {
        public MockCollaborationSharingRepository MockAddCollaborationSharing(CollaborationSharingDTO result)
        {
            Setup(x => x.AddCollaborationSharing(It.IsAny<CollaborationSharingDTO>()))
                .Returns(result);
            return this;
        }
        public MockCollaborationSharingRepository MockEditCollaborationSharing(CollaborationSharingDTO result)
        {
            Setup(x => x.UpdateCollaborationSharing(It.IsAny<CollaborationSharingDTO>()))
               .Returns(result);

            Setup(x => x.GetCollaborationSharing(It.IsAny<Guid>()))
                .Returns(Task.FromResult(result));
            return this;
        }
    }
}
