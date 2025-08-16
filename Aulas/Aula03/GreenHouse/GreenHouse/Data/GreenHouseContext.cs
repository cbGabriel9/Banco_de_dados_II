using GreenHouse.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenHouse.Data
{
    public class GreenHouseContext : DbContext
    {
        public GreenHouseContext(DbContextOptions<GreenHouseContext> options) : base(options)
        {

        }

        public DbSet<Plant> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>().ToTable("Plant");
        }
    }
}
