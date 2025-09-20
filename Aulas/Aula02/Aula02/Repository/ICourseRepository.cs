using Aula02.Models;

namespace Aula02.Repository
{
    public interface ICourseRepository
    {
            public Task Create(Course course);
            public Task Update(Course course);
            public Task Delete(Course course);
            public Task<Course?> GetById(int id);
            public Task<List<Course>> GetByName(string name);
            public Task<List<Course>> GetAll();
    }
}
