using DreamGarden.Data;
using DreamGarden.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamGarden.Repositories
{
    public class HomeRepository : IHomeRepository //interface for the repository
    {
        private readonly ApplicationDbContext _db;
        //constructor is needed to inject database context
        public HomeRepository(ApplicationDbContext db)
        {
            //connection to the database
            _db = db; 

        }


        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }

        //create asynchronous method which is expected to return a list of flowers with two optional parameters
        public async Task <IEnumerable<Flower>> GetFlowers(string sTerm = "", int genreId = 0)
        {
            //case insensitive search
            sTerm = sTerm.ToLower();
            IEnumerable<Flower> flowers=await (from flower in _db.Flowers
            //all columns are from Flower DB except for Genre which is from Genre DB
                           join genre in _db.Genres on flower.GenreId equals genre.Id
                           //join the stock DB
                           join stock in _db.Stocks on flower.Id equals stock.FlowerId into flower_stocks
                           from flowerWithStock in flower_stocks.DefaultIfEmpty()
                           //if the search teris empty or the flower name contains the search term
                           //where (string.IsNullOrEmpty(sTerm) || Flower.FlowerName.ToLower().Contains(sTerm)) && (genreId == 0 || Flower.GenreId == genreId)
                           where (string.IsNullOrWhiteSpace(sTerm) || (flower.FlowerName.ToLower().Contains(sTerm)) && flower!=null)
                           select new Flower
                           {
                               Id=flower.Id,
                               Image = flower.Image,
                               FlowerName =flower.FlowerName,
                               LatinName=flower.LatinName,
                               Description=flower.Description,
                               GenreId=flower.GenreId,
                               Price=flower.Price,
                               GenreName=genre.GenreName,
                               Quantity=flower.Stock==null?0:flower.Stock.Quantity
                           }
                           ).ToListAsync();

           
            //you want to filter after the genre
            if(genreId>0)
            {
                //if the genre ID is greater than 0, filter the flowers by genre ID
                flowers=flowers.Where(a=>a.GenreId == genreId).ToList();    

            }

            return flowers;

        }
    }
}
