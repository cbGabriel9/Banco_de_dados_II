using Aula02.Models;

namespace Aula02.Repository
{
    public interface IStudentCoursesRepository
    {
        public Task Create(StudentCoursesRepository studentCourse);
        public Task Update(StudentCoursesRepository studentCourse);
        public Task Delete(StudentCoursesRepository studentCourse);
        public Task<List<StudentCoursesRepository?>> GetByStudentId(int studentId);
        public Task<List<StudentCoursesRepository?>> GetByCourseId(int courseId);
        public Task<StudentCoursesRepository?> Get(int studentId, int courseId);
        public Task<List<StudentCoursesRepository>> GetByCourseName(string name);
        public Task<List<StudentCoursesRepository>> GetByStudentName(string name);
        public Task<List<StudentCoursesRepository>> GetAll();
    }
}
