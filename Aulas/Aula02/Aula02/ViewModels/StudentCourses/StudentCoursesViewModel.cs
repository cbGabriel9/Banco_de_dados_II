using Aula02.Models;
using System.Xml.Serialization;

namespace Aula02.ViewModels.StudentCourses
{
    public class StudentCoursesViewModel
    {
        public int StudentId { get; set; }
        public List<Student> Students { get; set; } = [];
        public List<SelectedCourses> Courses { get; set; } = [];

        public void SetCourses(List<Course> courses)
        {
            Courses = [.. courses.Select(c => new SelectedCourses
                {
                    Id = c.ID,
                    Name = c.Name!,
                    IsSelected = false
                })
            ];
        }
    }

    public class SelectedCourses
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
