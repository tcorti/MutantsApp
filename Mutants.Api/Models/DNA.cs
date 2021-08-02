using System;

namespace Mutants.Models
{
    public class DNA
    {
        public int Id {get; init;}
        public string DnaStrings {get; init;}
        public bool IsMutant {get; init;}
        public DateTime Date { get; init; }
    }
}