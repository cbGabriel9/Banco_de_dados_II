using GreenHouse.Models;

namespace GreenHouse.Data
{
    public class DbInitializer
    {
        public static void Initialize(GreenHouseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Plants.Any())
            {
                return;   // DB has been seeded
            }

            var plants = new Plant[]
            {
            new Plant{PlantName = "Orquídea"},
            new Plant{PlantName = "Cacto"},
            new Plant{PlantName = "Girassol"},
            new Plant{PlantName = "Rosa Vermelha"},
            new Plant{PlantName = "Trigo"}
            };
            foreach (Plant p in plants)
            {
                context.Plants.Add(p);
            }
            context.SaveChanges();
        }
    }
}
