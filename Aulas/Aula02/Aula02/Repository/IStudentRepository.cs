using Aula02.Models;

namespace Aula02.Repository
{
    public interface IStudentRepository
    {
        public Task Create(Student student);
        public Task Update(Student student);
        public Task Delete(Student student);
        public Task<Student?> GetById(int id);
        public Task<List<Student>> GetByName(string name);
        public Task<List<Student>> GetAllNotEnrolled();
        public Task<List<Student>> GetAll();
    }
}
