using Microsoft.Extensions.Logging;
using Mutants.Context;
using Mutants.Models.DTOs;
using Mutants.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutants.Services
{
    public class StatsService
    {
        private readonly AppDbContext _appDbContext;
        public StatsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual StatsResponse GetStats()
        {
            var humanCount = _appDbContext.Dna.Count(x => x.IsMutant == false);
            var mutantCount = _appDbContext.Dna.Count(x => x.IsMutant == true);
            var ratio = (humanCount != 0 && mutantCount != 0) ? decimal.Divide(mutantCount, humanCount) : 0;
            ratio = decimal.Round(ratio, 2, MidpointRounding.AwayFromZero);

            return new StatsResponse()
            {
                count_human_dna = humanCount,
                count_mutant_dna = mutantCount,
                ratio = ratio
            };
        }
    }
}
