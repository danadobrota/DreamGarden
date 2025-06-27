using Microsoft.EntityFrameworkCore;

namespace DreamGarden.Repositories
{

    public class GenreRepository : IGenreRepository //implement the defined interface
    {
        private readonly ApplicationDbContext _context;

        //inject the ApplicationDbContext into the repository through the constructor
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //implement all the methods defined in the interface, this ones will be managable by the Admin Interface only
        public async Task AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }
        public async Task<Genre?> GetGenreById(int id) =>
            await _context.Genres.FindAsync(id);
        public async Task DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Genre>> GetGenres() =>
            await _context.Genres.ToListAsync();
    }
}
