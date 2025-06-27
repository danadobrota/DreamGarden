//will define the contract between the application and the database for flower retrieval.
namespace DreamGarden
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Flower>> GetFlowers(string sTerm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}