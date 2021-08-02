using Microsoft.AspNetCore.Mvc;
using Moq;
using Mutants.Context;
using Mutants.Controllers;
using Mutants.Models.DTOs;
using Mutants.Services;
using Xunit;

namespace Mutants.UnitTests
{
    public class StatsControllerTest
    {
        [Fact]
        public void GetStats()
        {
            //Arrange
            var mockStatsResponse = new StatsResponse()
            {
                count_human_dna = 100,
                count_mutant_dna = 40,
                ratio = (decimal)0.4
            };
            var statsServiceStub = CreateStatsServiceStub();
            statsServiceStub.Setup(service => service.GetStats())
                .Returns(mockStatsResponse);

            var controller = new StatsController(statsServiceStub.Object);

            //Act
            var result = controller.GetStats();
            var okResult = (result != null && result.Result != null) ?  result.Result as OkObjectResult : null;
            var statsResult = (okResult != null && okResult.Value != null) ? okResult.Value as StatsResponse : null;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.NotNull(statsResult);
            Assert.Equal(100, statsResult.count_human_dna);
            Assert.Equal(40, statsResult.count_mutant_dna);
            Assert.Equal((decimal)0.4, statsResult.ratio);
        }

        private static Mock<StatsService> CreateStatsServiceStub()
        {
            var dbContextStub = new Mock<AppDbContext>();
            return new Mock<StatsService>(dbContextStub.Object);
        }

    }
}
