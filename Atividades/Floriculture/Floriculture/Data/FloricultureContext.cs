using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Floriculture.Models;

namespace Floriculture.Data
{
    public class FloricultureContext : DbContext
    {
        public FloricultureContext(DbContextOptions<FloricultureContext> options) : base(options)
        {

        }

        public DbSet<Plant> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>().ToTable("Plant");
        }
    }
}
