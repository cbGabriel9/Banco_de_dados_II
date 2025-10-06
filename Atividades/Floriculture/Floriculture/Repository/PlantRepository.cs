using Floriculture.Data;
using Floriculture.Models;
using Microsoft.EntityFrameworkCore;

namespace Floriculture.Repository
{
    public class PlantRepository : IPlantRepository
    {
        private readonly FloricultureContext _context;

        public PlantRepository(FloricultureContext context)
        {
            _context = context;
        }

        public async Task Create(Plant plant)
        {
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();
        }
        public Task Update(Plant plant)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Plant plant)
        {
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Plant>> GetAll()
        {
            var data = await _context.Plants
                .ToListAsync();

            return data;
        }

        public async Task<Plant?> GetById(int id)
        {
            var plant = await _context.Plants.Where(p => p.ID == id).FirstOrDefaultAsync();

            return plant;
        }

    }
}
