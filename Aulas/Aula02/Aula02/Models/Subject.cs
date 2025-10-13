using System.ComponentModel.DataAnnotations;

namespace Aula02.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
