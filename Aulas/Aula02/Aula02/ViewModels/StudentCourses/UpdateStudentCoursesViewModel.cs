using Aula02.Models;
using System.Xml.Serialization;

namespace Aula02.ViewModels.StudentCourses
{
    public class UpdateStudentCoursesViewModel
    {
        public Student? SelectedStudent { get; set; }
        public List<SelectedCourses> Courses { get; set; } = [];

        public void SetCourses(List<Course> courses)
        {
            Courses = [.. courses.Select(c => new SelectedCourses
                {
                    Id = c.ID,
                    Name = c.Name!
                })
            ];
        }
    }
}
