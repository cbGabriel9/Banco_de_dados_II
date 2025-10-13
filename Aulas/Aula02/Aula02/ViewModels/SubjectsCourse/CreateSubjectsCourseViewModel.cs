using Aula02.Models;
using System.Xml.Serialization;

namespace Aula02.ViewModels.SubjectsCourse
{
    public class CreateSubjectsCourseViewModel
    {
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; } = [];
        public List<SelectedSubjects> Subjects { get; set; } = [];

        public void SetSubjects(List<Subject> subjects)
        {
            Subjects = [.. subjects.Select(c => new SelectedSubjects
                {
                    Id = c.ID,
                    Name = c.Name!
                })
            ];
        }
    }
}
