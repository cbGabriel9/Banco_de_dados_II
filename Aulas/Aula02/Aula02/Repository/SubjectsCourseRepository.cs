using Aula02.Data;
using Aula02.Models;
using Aula02.ViewModels.SubjectsCourse;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula02.Repository
{
    public class SubjectsCourseRepository : ISubjectsCourseRepository
    {
        private readonly SchoolContext _context;

        public SubjectsCourseRepository(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        // --- MÉTODOS DE MANIPULAÇÃO DE DADOS (CUD) ---

        public async Task Create(SubjectsCourse studentCourses)
        {
            await _context.SubjectsCourse.AddAsync(studentCourses);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int courseId, List<int> selectedCourseIds) // Caio aqui após chamar esse método no controller
        {
            var currentEnrollments = await _context.SubjectsCourse // Crio uma lista com todas as matrículas daquele estudante (courseId e subjectId são os mais importantes)
                .Where(sc => sc.CourseID == courseId)
                .ToListAsync();

            var currentCourseIds = currentEnrollments.Select(sc => sc.SubjectID).ToList(); // Separo em uma variável uma lista dos cursos atuais

            var subjectIdsToAdd = selectedCourseIds.Except(currentCourseIds).ToList(); // Crio uma lista de ids de cursos para adicionar, filtrando apenas os que não estão matriculados ainda
            foreach (var subjectId in subjectIdsToAdd)
            {
                var newEnrollment = new SubjectsCourse
                {
                    CourseID = courseId,
                    SubjectID = subjectId
                };

                await _context.SubjectsCourse.AddAsync(newEnrollment);
            }

            var subjectIdsToRemove = currentCourseIds.Except(selectedCourseIds).ToList();
            var enrollmentsToRemove = currentEnrollments
                .Where(sc => subjectIdsToRemove.Contains(sc.SubjectID))
                .ToList();

            _context.SubjectsCourse.RemoveRange(enrollmentsToRemove);

            await _context.SaveChangesAsync();
        }
        public async Task Delete(SubjectsCourse studentCourses)
        {
            _context.SubjectsCourse.Remove(studentCourses);
            await _context.SaveChangesAsync();
        }

        // --- MÉTODOS DE CONSULTA (READ) ---

        public async Task<SubjectsCourse?> Get(int courseId, int subjectId)
        {
            return await _context.SubjectsCourse
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(w => w.CourseID == courseId && w.SubjectID == subjectId);
        }

        public async Task<List<SubjectsCourse>> GetAll()
        {
            return await _context.SubjectsCourse
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .ToListAsync();
        }

        public async Task<List<SubjectsCourse>> GetByCourseId(int subjectId)
        {
            return await _context.SubjectsCourse
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .Where(w => w.SubjectID == subjectId)
                .ToListAsync();
        }

        public async Task<List<SubjectsCourse>> GetBySubjectId(int courseId)
        {
            return await _context.SubjectsCourse
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .Where(w => w.CourseID == courseId)
                .ToListAsync();
        }

        public async Task<List<SubjectsCourse>> GetByCourseName(string courseName)
        {
            return await _context.SubjectsCourse
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .Where(w => w.Course != null && w.Course.Name != null && w.Course.Name.ToLower().Contains(courseName.ToLower()))
                .ToListAsync();
        }

        public async Task<List<SubjectsCourse>> GetBySubjectName(string name)
        {
            return await _context.SubjectsCourse
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .Where(w => w.Course != null && w.Course.Name != null && w.Course.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}