using Floriculture.Models;

namespace Floriculture.Repository
{
    public interface IPlantRepository
    {
        public Task Create(Plant plant);
        public Task Update(Plant plant);
        public Task Delete(Plant plant);
        public Task GetById(int id);
    }
}
