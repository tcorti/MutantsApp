using Microsoft.EntityFrameworkCore;
using Mutants.Models;

namespace Mutants.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext() : base()
        {

        }
        public DbSet<DNA> Dna {get; set;}
    }
}
