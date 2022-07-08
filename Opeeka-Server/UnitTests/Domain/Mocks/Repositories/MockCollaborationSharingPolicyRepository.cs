// -----------------------------------------------------------------------
// <copyright file="MockCollaborationSharingPolicyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockCollaborationSharingPolicyRepository : Mock<ICollaborationSharingPolicyRepository>
    {
        public MockCollaborationSharingPolicyRepository MockAddCollaborationSharingPolicy(CollaborationSharingPolicyDTO result)
        {
            Setup(x => x.AddCollaborationSharingPolicy(It.IsAny<CollaborationSharingPolicyDTO>()))
                .Returns(result);
            return this;
        }
        public MockCollaborationSharingPolicyRepository MockEditCollaborationSharingPolicy(CollaborationSharingPolicyDTO result)
        {
            Setup(x => x.UpdateCollaborationSharingPolicy(It.IsAny<CollaborationSharingPolicyDTO>()))
               .Returns(result);

            Setup(x => x.GetCollaborationSharingPolicy(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }
    }
}
