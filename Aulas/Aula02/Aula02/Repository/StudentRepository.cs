using Aula02.Data;
using Aula02.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aula02.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context; // Armazeno o contexto para ser utilizado no StudentRepository
        }
        public async Task Create(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAll()
        {
            var data = await _context.Students.ToListAsync();
            return data;
        }

        public async Task<Student?> GetById(int id)
        {
            var student = await _context.Students.Where(s => s.ID == id).FirstOrDefaultAsync();

            return student;
        }

        public async Task<List<Student>> GetByName(string name)
        {
            var students = await _context.Students.Where(s => s.FirstMidName!.ToLower().Contains(name.ToLower())).ToListAsync();

            return students;
        }
    }
}
