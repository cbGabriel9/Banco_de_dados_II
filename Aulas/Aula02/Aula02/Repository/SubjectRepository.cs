using Aula02.Data;
using Aula02.Models;
using Microsoft.EntityFrameworkCore;

namespace Aula02.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolContext _context;

        public SubjectRepository(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        public async Task Create(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subject subject)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetAll()
        {
            var data = await _context.Subjects.ToListAsync();
            return data;
        }

        public async Task<Subject?> GetById(int id)
        {
            var subject = await _context.Subjects.Where(s => s.ID == id).FirstOrDefaultAsync();

            return subject;
        }
    }
}
