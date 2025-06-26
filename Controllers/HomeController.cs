using System.Diagnostics;
using DreamGarden.Models;
using DreamGarden.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DreamGarden.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        //inject the home repository in our controller
        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)   
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="", int genreId=0)
        {
            //call the repository method to get the flowers
            IEnumerable<Flower> flowers = await _homeRepository.GetFlowers(sterm, genreId);

            //get the genres from the repository
            IEnumerable<Genre> genres = await _homeRepository.Genres();

            //create a new instance of the Flower model to hold the search term and genre ID
            FlowerDisplayModel flowerModel = new FlowerDisplayModel
            {
                Flowers = flowers,
                Genres = genres,
                STerm=sterm,
                GenreId=genreId
            };

            return View(flowerModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
