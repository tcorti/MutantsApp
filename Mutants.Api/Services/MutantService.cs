using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mutants.Context;
using Mutants.Models;
using Mutants.Models.DTOs;

namespace Mutants.Services
{
    public class MutantService
    {
        private readonly ILogger<MutantService> _logger;

        private readonly AppDbContext _appDbContext;
        public MutantService(ILogger<MutantService> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public virtual async Task<bool> IsMutantAsync(IsMutantRequest rq)
        {

            if (!ValidateRequest(rq))
                return false;

            var length = rq.dna.Length;
            var mutantSeqcount = 0;
            char[][] dnaMap = rq.dna.Select(item => item.ToArray()).ToArray();

            for (int i = 0; i < length; i++)
            {
                //Horizontal search.
                var row = dnaMap[i];
                mutantSeqcount = AnalyzeSequence(row, mutantSeqcount);
                if (mutantSeqcount >= 2) return await RegisterDnaInDbAsync(rq.dna, true);

                //Vertical search.
                var column = Enumerable.Range(0, length)
                .Select(x => dnaMap[x][i])
                .ToArray();
                mutantSeqcount = AnalyzeSequence(column, mutantSeqcount);
                if (mutantSeqcount >= 2) return await RegisterDnaInDbAsync(rq.dna, true);

            }

            for (int i = -length + 4; Math.Abs(i) < length - 3; i++)
            {
                //First diagonal Search
                var diagonal = GetDiagonal(dnaMap, i, length, 1);
                mutantSeqcount = AnalyzeSequence(diagonal, mutantSeqcount);
                if (mutantSeqcount >= 2) return await RegisterDnaInDbAsync(rq.dna, true);

                //Second diagonal Search
                diagonal = GetDiagonal(dnaMap, i, length, 2);
                mutantSeqcount = AnalyzeSequence(diagonal, mutantSeqcount);
                if (mutantSeqcount >= 2) return await RegisterDnaInDbAsync(rq.dna, true);

            }

            return await RegisterDnaInDbAsync(rq.dna, false);
        }

        private static bool ValidateRequest(IsMutantRequest rq)
        {
            var validChars = new List<char> {'C','G','T','A' };
            var invalidChar = rq.dna.FirstOrDefault(x => x.ToArray().Any(c => !validChars.Contains(c)));
            if (invalidChar != null) return false;

            return true;
        }

        private static char[] GetDiagonal(char[][] dnaMap, int i, int n, int direction)
        {
            char[] diagonal = new char[n - Math.Abs(i)];
            for (int j = 0; j <= n - Math.Abs(i) - 1; j++)
            {
                int row = direction == 1 ? (i < 0 ? j : i + j)                                  //Primary diagonal row index.
                                         : (i < 0 ? ((n - 1) - Math.Abs(i) - j) : (n - 1) - j); //Secondary diagonal row index.

                int col = direction == 1 ? (i > 0 ? j : (Math.Abs(i) + j))  //Primary diagonal col index.
                                         : (i > 0 ? j + i : j);             //Secondary diagonal col index.
                diagonal[j] = dnaMap[row][col];
            }
            return diagonal;
        }

        private static int AnalyzeSequence(char[] word, int count)
        {
            var repeatedCount = 0;
            for (int i = 1; (i < word.Length) && (count < 2); i++)
            {
                if (word[i - 1] == word[i])
                    repeatedCount++;
                else
                    repeatedCount = 0;

                if (repeatedCount >= 3)
                {
                    count++;
                    i++;
                }
            }

            return count;
        }

        private async Task<bool> RegisterDnaInDbAsync(string[] dnaString, bool isMutant)
        {
            var dna = new DNA()
            {
                DnaStrings = string.Join(",", dnaString),
                IsMutant = isMutant,
                Date = DateTime.Now
            };

            try
            {
                await _appDbContext.Dna.AddAsync(dna);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed saving Dna with exception: {e}");
            }

            return isMutant;
        }

    }
}