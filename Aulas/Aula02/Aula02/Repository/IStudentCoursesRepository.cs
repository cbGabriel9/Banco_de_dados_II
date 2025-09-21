using Aula02.Models;

namespace Aula02.Repository
{
    public interface IStudentCoursesRepository
    {
        public Task Create(StudentCourses studentCourse);
        public Task Update(int? originalStudentId, int? originalCourseId, StudentCourses studentCourseNewData);
        public Task Delete(StudentCourses studentCourse);
        public Task<List<StudentCourses?>> GetByStudentId(int studentId);
        public Task<List<StudentCourses?>> GetByCourseId(int courseId);
        public Task<StudentCourses?> Get(int studentId, int courseId);
        public Task<List<StudentCourses>> GetByCourseName(string name);
        public Task<List<StudentCourses>> GetByStudentName(string name);
        public Task<List<StudentCourses>> GetAll();
    }
}
