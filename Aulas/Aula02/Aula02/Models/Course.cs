using System.ComponentModel.DataAnnotations;

namespace Aula02.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<StudentCourses>? StudentCourses { get; set; } // Indica que tem vários cursos na tabela StudentCourses
    }
}
