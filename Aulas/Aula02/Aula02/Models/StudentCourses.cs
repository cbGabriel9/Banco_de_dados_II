using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula02.Models
{
    [PrimaryKey(nameof(StudentID), nameof(CourseID))]
    public class StudentCourses
    {
        public int StudentID { get; set; }

        // Property Navigations 
        [ForeignKey(nameof(StudentID))]
        public Student? Student { get; set; }

        public int CourseID { get; set; }

        // Property Navigations 
        [ForeignKey(nameof(CourseID))]
        public Course? Course { get; set; }

        
    }
}
