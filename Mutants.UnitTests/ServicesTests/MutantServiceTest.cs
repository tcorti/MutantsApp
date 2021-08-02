using Microsoft.Extensions.Logging;
using Moq;
using Mutants.Context;
using Mutants.Models.DTOs;
using Mutants.Services;
using System.Threading.Tasks;
using Xunit;

namespace Mutants.UnitTests
{
    public class MutantServiceTest
    {
        [Fact]
        public async Task IsMutant_WithHumanDna_ReturnsFalseAsync()
        {
            //Arrange
            var mutantService = CreateMutantService();

            string[] humanDna = { "TTGCTA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };

            IsMutantRequest humanDnaRq = new IsMutantRequest()
            {
                dna = humanDna
            };

            //Act
            var result = await mutantService.IsMutantAsync(humanDnaRq);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsMutant_WithMutantDna_ReturnsTrueAsync()
        {
            //Arrange
            var mutantService = CreateMutantService();

            string[] mutantDna = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };

            IsMutantRequest mutantDnaRq = new IsMutantRequest()
            {
                dna = mutantDna
            };

            //Act
            var result = await mutantService.IsMutantAsync(mutantDnaRq);

            //Assert
            Assert.True(result);
        }

        private static MutantService CreateMutantService()
        {
            var dbContextStub = new Mock<AppDbContext>();
            var loggerStub = new Mock<ILogger<MutantService>>();
            return new MutantService(loggerStub.Object, dbContextStub.Object);
        }
    }
}
