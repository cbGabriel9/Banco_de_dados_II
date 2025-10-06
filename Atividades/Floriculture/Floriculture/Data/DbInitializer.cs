using Floriculture.Models;

namespace Floriculture.Data
{
    public class DbInitializer
    {
        public static void Initialize(FloricultureContext context)
        {
            context.Database.EnsureCreated();

            if (context.Plants.Any())
            {
                return;   // DB has been seeded
            }

            var plants = new Plant[]
            {
                new Plant { Name = "Orquídea Phalaenopsis", SensorValue = 25.5f, SensorEvent = new DateTime(2025, 12, 20, 9, 30, 0) },
                new Plant { Name = "Samambaia Americana", SensorValue = 60.2f, SensorEvent = DateTime.Now.AddMinutes(-55) },
                new Plant { Name = "Suculenta Echeveria", SensorValue = 15.0f, SensorEvent = DateTime.Now.AddMinutes(-10) },
                new Plant { Name = "Cacto Estrela", SensorValue = 10.1f, SensorEvent = DateTime.Now.AddMinutes(-8) },
                new Plant { Name = "Monstera Deliciosa", SensorValue = 45.7f, SensorEvent = DateTime.Now.AddMinutes(-40) },
                new Plant { Name = "Jiboia Verde", SensorValue = 55.8f, SensorEvent = DateTime.Now.AddMinutes(-50) },
                new Plant { Name = "Lírio da Paz", SensorValue = 30.3f, SensorEvent = DateTime.Now.AddMinutes(-28) },
                new Plant { Name = "Espada de São Jorge", SensorValue = 20.9f, SensorEvent = DateTime.Now.AddMinutes(-18) },
                new Plant { Name = "Alecrim", SensorValue = 35.1f, SensorEvent = DateTime.Now.AddMinutes(-30) },
                new Plant { Name = "Hortelã", SensorValue = 40.4f, SensorEvent = DateTime.Now.AddMinutes(-38) }
            };

            foreach (Plant p in plants)
            {
                context.Plants.Add(p);
            }

            context.SaveChanges();
        }
    }
}
