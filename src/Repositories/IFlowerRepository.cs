namespace DreamGarden.Repositories
{
    public interface IFlowerRepository
    {
        Task AddFlower(Flower flower);
        Task DeleteFlower(Flower flower);
        Task<Flower?> GetFlowerById(int id);

        Task<IEnumerable<Flower>> GetFlowers();

        Task UpdateFlower(Flower flower);
    }
}
