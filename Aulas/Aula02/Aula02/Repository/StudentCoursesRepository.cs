using Aula02.Data;
using Aula02.Models;
using Microsoft.EntityFrameworkCore;

namespace Aula02.Repository
{
    public class StudentCoursesRepository : IStudentCoursesRepository
    {
        private readonly SchoolContext _context;

        public StudentCoursesRepository(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        public async Task Create(StudentCourses studentCourses)
        {
            await _context.StudentCourses.AddAsync(studentCourses);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(StudentCourses studentCourses)
        {
            _context.StudentCourses.Remove(studentCourses);
            await _context.SaveChangesAsync();
        }

        public async Task Update(StudentCourses studentCourses)
        {
            _context.StudentCourses.Update(studentCourses);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentCourses?> Get(int studentId, int courseId)
        {
            var data = await _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(w => w.StudentID == studentId && w.CourseID == courseId);

            return data;
        }
        public async Task<List<StudentCourses?>> GetAll()
        {
            var data = await _context.StudentCourses.Include(x => x.Course).Include(x => x.Student);

            return data;
        }

        public async Task<List<StudentCourses?>> GetByCourseId(int courseId)
        {
            var data = await _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(w => w.CourseID == courseId);

            return data;
        }

        public async Task<List<StudentCourses?>> GetByStudentId(int studentId)
        {
            var data = await _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(w => w.StudentID == studentId);

            return data;
        }

        public async Task<List<StudentCourses?>> GetByCourseName(string courseName)
        {
            var data = await _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(w => w.Course!.Name!.ToLower().Contains(Name.ToLower()));

            return data;
        }

        public async Task<List<StudentCourses?>> GetByStudentName(string name)
        {
            var data = await _context.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(w => w.StudentID == studentId);

            return data;
        }

    }
}
