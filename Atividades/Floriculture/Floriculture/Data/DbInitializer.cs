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
                new Plant { Name = "Orquídea Phalaenopsis", SensorValue = 25.5f, SensorEvent = 20.0f },
                new Plant { Name = "Samambaia Americana", SensorValue = 60.2f, SensorEvent = 55.0f },
                new Plant { Name = "Suculenta Echeveria", SensorValue = 15.0f, SensorEvent = 10.0f },
                new Plant { Name = "Cacto Estrela", SensorValue = 10.1f, SensorEvent = 8.0f },
                new Plant { Name = "Monstera Deliciosa", SensorValue = 45.7f, SensorEvent = 40.0f },
                new Plant { Name = "Jiboia Verde", SensorValue = 55.8f, SensorEvent = 50.0f },
                new Plant { Name = "Lírio da Paz", SensorValue = 30.3f, SensorEvent = 28.0f },
                new Plant { Name = "Espada de São Jorge", SensorValue = 20.9f, SensorEvent = 18.0f },
                new Plant { Name = "Alecrim", SensorValue = 35.1f, SensorEvent = 30.0f },
                new Plant { Name = "Hortelã", SensorValue = 40.4f, SensorEvent = 38.0f }
            };

            foreach (Plant p in plants)
            {
                context.Plants.Add(p);
            }

            context.SaveChanges();
        }
    }
}
