using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gwp.BusinessLogic.GrossWrittenPremium;
using Gwp.DataAccess.Entities;
using Gwp.DataAccess.Repositories;
using Moq;
using NUnit.Framework;

namespace Gwp.BusinessLogic.Tests
{
    public class GrossWrittenPremiumServiceTests
    {
        private Mock<IGrossWrittenPremiumRepository> _repositoryMock;

        [Test]
        public async Task GetAverage_ComputesCorrectAvg()
        {
            // arrange
            var service = new GrossWrittenPremiumService(_repositoryMock.Object);

            //act
            var result = await service.GetAverageAsync(new List<int> {1}, 1, 2000, 2022);

            //assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(300, result.First().Average);
        }

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGrossWrittenPremiumRepository>();
            var gwps = new List<DataAccess.Entities.GrossWrittenPremium>
            {
                new DataAccess.Entities.GrossWrittenPremium
                {
                    Amount = 100, Country = new Country {Id = 1}, LineOfBusiness = new LineOfBusiness {Id = 1}, Year = 2000
                },
                new DataAccess.Entities.GrossWrittenPremium
                {
                    Amount = 500, Country = new Country {Id = 1}, LineOfBusiness = new LineOfBusiness {Id = 1}, Year = 2022
                },
                new DataAccess.Entities.GrossWrittenPremium
                {
                    Amount = 1000, Country = new Country {Id = 1}, LineOfBusiness = new LineOfBusiness {Id = 1}, Year = 2050
                },
            };
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(gwps);
        }
    }
}