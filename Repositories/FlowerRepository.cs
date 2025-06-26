using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;

namespace DreamGarden.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly ApplicationDbContext _context;
        public FlowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddFlower(Flower flower)
        {
            _context.Flowers.Add(flower);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlower(Flower flower)
        {
            _context.Flowers.Update(flower);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteFlower(Flower flower)
        {
            _context.Flowers.Remove(flower);
            await _context.SaveChangesAsync();
        }
        public async Task<Flower?> GetFlowerById(int id) => 
            await _context.Flowers.FindAsync(id);

        public async Task<IEnumerable<Flower>> GetFlowers() =>
            await _context.Flowers.Include(a=>a.Genre).ToListAsync();

    }
}
