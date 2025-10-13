using Aula02.Data;
using Aula02.Models;
using Microsoft.EntityFrameworkCore;

namespace Aula02.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        public async Task Create(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll()
        {
            var data = await _context.Courses.ToListAsync();
            return data;
        }

        public async Task<Course?> GetById(int id)
        {
            var course = await _context.Courses.Where(s => s.ID == id).FirstOrDefaultAsync();

            return course;
        }

        public async Task<List<Course>> GetByName(string name)
        {
            var courses = await _context.Courses.Where(s => s.Name!.ToLower().Contains(name.ToLower())).ToListAsync();

            return courses;
        }

        public async Task<List<Course>> GetAllNotEnrolled()
        {
            var enrolledCourseIds = await _context.SubjectsCourse
                .Select(sc => sc.CourseID)
                .Distinct()
                .ToListAsync();

            var data = await _context.Courses
                .Include(sc => sc.SubjectsCourse!)
                    .ThenInclude(c => c.Course)
                    .Where(w => !enrolledCourseIds.Contains(w.ID))
                    .OrderBy(c => c.Name)
                .ToListAsync();
            return data;
        }
    }
}
