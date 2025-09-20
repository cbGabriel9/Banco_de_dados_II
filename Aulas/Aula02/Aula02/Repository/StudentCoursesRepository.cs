using Aula02.Data;
using Aula02.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula02.Repository
{
    public class StudentCoursesRepository : IStudentCoursesRepository
    {
        private readonly SchoolContext _context;

        public StudentCoursesRepository(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        // --- MÉTODOS DE MANIPULAÇÃO DE DADOS (CUD) ---

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

        // --- MÉTODOS DE CONSULTA (READ) ---

        public async Task<StudentCourses?> Get(int studentId, int courseId)
        {
            return await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .FirstOrDefaultAsync(w => w.StudentID == studentId && w.CourseID == courseId);
        }

        public async Task<List<StudentCourses>> GetAll()
        {
            return await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .ToListAsync();
        }

        public async Task<List<StudentCourses>> GetByCourseId(int courseId)
        {
            return await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.CourseID == courseId)
                .ToListAsync();
        }

        public async Task<List<StudentCourses>> GetByStudentId(int studentId)
        {
            return await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.StudentID == studentId)
                .ToListAsync();
        }

        public async Task<List<StudentCourses>> GetByCourseName(string courseName)
        {
            return await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.Course != null && w.Course.Name != null && w.Course.Name.ToLower().Contains(courseName.ToLower()))
                .ToListAsync();
        }

        public async Task<List<StudentCourses>> GetByStudentName(string name)
        {
            return await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.Student != null && w.Student.FirstMidName != null && w.Student.FirstMidName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}