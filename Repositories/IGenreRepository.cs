namespace DreamGarden.Repositories
{
    public interface IGenreRepository
    {
        // Define methods for genre operations, e.g., GetGenres, AddGenre, UpdateGenre, DeleteGenre
        Task AddGenre(Genre genre);

        Task UpdateGenre(Genre genre);

        Task<Genre?> GetGenreById(int id);

        Task DeleteGenre(Genre genre);

        Task<IEnumerable<Genre>> GetGenres();
    }
}
