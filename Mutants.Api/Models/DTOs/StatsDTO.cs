namespace Mutants.Models.DTOs
{
    public class StatsResponse
    {
        public int count_mutant_dna { get; init; }
        public int count_human_dna { get; init; }
        public decimal ratio { get; init; }
    }
}
