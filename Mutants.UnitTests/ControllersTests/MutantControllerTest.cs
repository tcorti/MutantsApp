using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Mutants.Context;
using Mutants.Controllers;
using Mutants.Models.DTOs;
using Mutants.Services;
using System.Threading.Tasks;
using Xunit;

namespace Mutants.UnitTests
{
    public class MutantControllerTest
    {
        [Fact]
        public async Task IsMutant_WithHumanDna_Returns403Async()
        {
            //Arrange
            var mutantServiceStub = CreateMutantServiceStub();
            mutantServiceStub.Setup(service => service.IsMutantAsync(It.IsAny<IsMutantRequest>()))
                .ReturnsAsync(false);

            var mutantController = new MutantController(mutantServiceStub.Object);

            string[] testDna = { "TTGCTA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };

            IsMutantRequest testDnaRq = new IsMutantRequest()
            {
                dna = testDna
            };

            //Act
            var result = await mutantController.IsMutantAsync(testDnaRq);
            var statusCodeResult = (IStatusCodeActionResult)result;

            //Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(403, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task IsMutant_WithMutantDna_Returns200Async()
        {
            //Arrange
            var mutantServiceStub = CreateMutantServiceStub();
            mutantServiceStub.Setup(service => service.IsMutantAsync(It.IsAny<IsMutantRequest>()))
                .ReturnsAsync(true);

            var mutantController = new MutantController(mutantServiceStub.Object);

            string[] testDna = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };

            IsMutantRequest testDnaRq = new IsMutantRequest()
            {
                dna = testDna
            };

            //Act
            var result = await mutantController.IsMutantAsync(testDnaRq);
            var statusCodeResult = (IStatusCodeActionResult)result;

            //Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task IsMutant_WithNullRequest_Returns400Async()
        {
            //Arrange
            var mutantServiceStub = CreateMutantServiceStub();

            var mutantController = new MutantController(mutantServiceStub.Object);

            IsMutantRequest testDnaRq = null;

            //Act
            var result = await mutantController.IsMutantAsync(testDnaRq);
            var statusCodeResult = (IStatusCodeActionResult)result;

            //Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        private static Mock<MutantService> CreateMutantServiceStub()
        {
            var dbContextStub = new Mock<AppDbContext>();
            var loggerStub = new Mock<ILogger<MutantService>>();
            var mutantServiceStub = new Mock<MutantService>(loggerStub.Object, dbContextStub.Object);
            return mutantServiceStub;
        }
    }
}
