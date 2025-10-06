using System.ComponentModel.DataAnnotations;

namespace Floriculture.Models
{
    public class Plant
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public float SensorValue { get; set; }
        public DateTime SensorEvent { get; set; }
    }
}
