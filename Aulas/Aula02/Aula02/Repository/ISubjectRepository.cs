using Aula02.Models;

namespace Aula02.Repository
{
    public interface ISubjectRepository
    {
            public Task Create(Subject subject);
            public Task Update(Subject subject);
            public Task Delete(Subject subject);
            public Task<Subject?> GetById(int id);
            public Task<List<Subject>> GetAll();
    }
}
