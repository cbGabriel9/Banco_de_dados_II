using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula02.Models
{
    [PrimaryKey(nameof(SubjectID), nameof(CourseID))]
    public class SubjectsCourse
    {
        public int SubjectID { get; set; }

        [ForeignKey(nameof(SubjectID))]
        public Subject? Subject { get; set; }
        public int CourseID { get; set; }

        [ForeignKey(nameof(CourseID))]
        public Course? Course { get; set; }
    }
}
