
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockSexualityRepository : Mock<ISexualityRepository>
    {
        public MockSexualityRepository MockEditSexuality(Sexuality result)
        {
            Setup(x => x.GetSexuality(It.IsAny<long>()))
                .Returns(Task.FromResult(result));

            Setup(x => x.UpdateSexuality(It.IsAny<Sexuality>()))
               .Returns(result);
            return this;
        }
        public MockSexualityRepository MockEditSexualityException()
        {
            Setup(x => x.GetSexuality(It.IsAny<long>()))
               .Throws<Exception>();
            Setup(x => x.UpdateSexuality(It.IsAny<Sexuality>()))
              .Throws<Exception>();
            return this;
        }

        public MockSexualityRepository MockAddSexuality(Sexuality result)
        {
            Setup(x => x.AddSexuality(It.IsAny<Sexuality>())).Returns(result);
            return this;
        }

        public MockSexualityRepository MockAddSexualityException()
        {
            Setup(x => x.AddSexuality(It.IsAny<Sexuality>())).Throws<Exception>();
            return this;
        }

        public MockSexualityRepository MockSexualityList(List<Sexuality> results)
        {
            Setup(x => x.GetSexualityList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetSexualityCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockSexualityRepository MockSexualityListException(List<Sexuality> results)
        {
            Setup(x => x.GetSexualityList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            //Setup(x => x.GetSexualityCount())
            //    .Returns(results.Count);
            return this;
        }
    }
}
