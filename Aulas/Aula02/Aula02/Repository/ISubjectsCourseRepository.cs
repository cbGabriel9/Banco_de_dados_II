using Aula02.Models;

namespace Aula02.Repository
{
    public interface ISubjectsCourseRepository
    {
        public Task Create(SubjectsCourse subjectsCourse);
        public Task Update(int subjectsId, List<int> selectedSubjectsIds);
        public Task Delete(SubjectsCourse subjectsCourse);
        public Task<List<SubjectsCourse?>> GetBySubjectId(int subjectsId);
        public Task<List<SubjectsCourse?>> GetByCourseId(int courseId);
        public Task<SubjectsCourse?> Get(int subjectsId, int courseId);
        public Task<List<SubjectsCourse>> GetByCourseName(string name);
        public Task<List<SubjectsCourse>> GetBySubjectName(string name);
        public Task<List<SubjectsCourse>> GetAll();
    }
}
