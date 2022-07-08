using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockRaceEthnicityRepository : Mock<IRaceEthnicityRepository>
    {

        public MockRaceEthnicityRepository MockRaceEthnicityList(List<RaceEthnicity> results)
        {
            Setup(x => x.GetRaceEthnicityList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetRaceEthnicityCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockRaceEthnicityRepository MockRaceEthnicityListException(List<RaceEthnicity> results)
        {
            Setup(x => x.GetRaceEthnicityList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetRaceEthnicityCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockRaceEthnicityRepository MockAddRaceEthnicity(RaceEthnicity result)
        {
            Setup(x => x.AddRaceEthnicity(It.IsAny<RaceEthnicity>()))
                .Returns(result);

            return this;
        }

        public MockRaceEthnicityRepository MockAddRaceEthnicityException()
        {
            Setup(x => x.AddRaceEthnicity(It.IsAny<RaceEthnicity>()))
                 .Throws<Exception>();
            return this;
        }

        public MockRaceEthnicityRepository MockUpdateRaceEthnicity(RaceEthnicity result)
        {
            Setup(x => x.UpdateRaceEthnicity(It.IsAny<RaceEthnicity>()))
               .Returns(result);
            Setup(x => x.GetRaceEthnicity(It.IsAny<Int64>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        public MockRaceEthnicityRepository MockUpdateRaceEthnicityException()
        {
            Setup(x => x.UpdateRaceEthnicity(It.IsAny<RaceEthnicity>()))
                .Throws<Exception>();

            Setup(x => x.GetRaceEthnicity(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }

        public MockRaceEthnicityRepository MockdeleteRaceEthnicity(RaceEthnicity result)
        {
            Setup(x => x.UpdateRaceEthnicity(It.IsAny<RaceEthnicity>()))
               .Returns(result);

            Setup(x => x.GetRaceEthnicity(It.IsAny<Int64>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockRaceEthnicityRepository MockDeleteRaceEthnicityException()
        {
            Setup(x => x.UpdateRaceEthnicity(It.IsAny<RaceEthnicity>()))
                .Throws<Exception>();

            Setup(x => x.GetRaceEthnicity(It.IsAny<Int64>()))
                 .Throws<Exception>();
            return this;
        }
    }
}
