using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DreamGarden.Shared;

namespace DreamGarden.Controllers;

[Authorize(Roles = nameof(Roles.Admin))] // Only users with the Admin role can access this controller

public class FlowerController : Controller
{
    private readonly IFlowerRepository _flowerRepo;
    private readonly IGenreRepository _genreRepo;
    private readonly IFileService _fileService;

    //constructor
    public FlowerController(IFlowerRepository flowerRepo, IGenreRepository genreRepo, IFileService fileService)
   
    {
        _flowerRepo = flowerRepo;
        _genreRepo = genreRepo;
        _fileService = fileService;
    }


    public async Task<IActionResult> Index()
    {
        var flowers = await _flowerRepo.GetFlowers();
        return View(flowers);
    }

    public async Task<IActionResult> AddFlower()
    {
        var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
        {
            Value = genre.Id.ToString(),
            Text = genre.GenreName
        });
        FlowerDTO flowerToAdd = new() { GenreList = genreSelectList };
        return View(flowerToAdd);
    }

    [HttpPost]

    public async Task<IActionResult> AddFlower(FlowerDTO flowerToAdd)
    {
        var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
        {
            Value = genre.Id.ToString(),
            Text = genre.GenreName
        });

        flowerToAdd.GenreList = genreSelectList;

        if (!ModelState.IsValid)
        {
            // If model state is not valid, return the view with the model
            return View(flowerToAdd);
        }
        try
        {
            // Handle file upload if a file is provided
            if (flowerToAdd.ImageFile != null)
            {
                string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];
                string imageName = await _fileService.SaveFile(flowerToAdd.ImageFile, allowedExtensions);
                flowerToAdd.Image = imageName;

            }

            Flower flower = new()
            {
                Id=flowerToAdd.Id,
                FlowerName = flowerToAdd.FlowerName,
                LatinName = flowerToAdd.LatinName,
                Description = flowerToAdd.Description,
                Price = flowerToAdd.Price,
                GenreId = flowerToAdd.GenreId,
                Image = flowerToAdd.Image
            };
            await _flowerRepo.AddFlower(flower);
            TempData["successMessage"] = "Flower was added successfully";
            return RedirectToAction(nameof(AddFlower));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = "Sorry, something went wrong !!";
            return View(flowerToAdd);
        }

        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = "File not found. Please upload a valid image file.";
            return View(flowerToAdd);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "An unexpected error occurred. Please try again later.";
            return View(flowerToAdd);
            //    return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> UpdateFlower(int id)
    {
        var flower = await _flowerRepo.GetFlowerById(id);
        if (flower == null)
        {
            throw new InvalidOperationException($"Flower with id: {id} not found");
            return RedirectToAction(nameof(Index));
        }
        var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
        {
            Value = genre.Id.ToString(),
            Text = genre.GenreName,
            Selected = flower.GenreId == genre.Id // Set the selected genre
        });
        FlowerDTO flowerToEdit = new()
        {
            Id = flower.Id,
            FlowerName = flower.FlowerName,
            LatinName = flower.LatinName,
            Description = flower.Description,
            Price = flower.Price,
            GenreId = flower.GenreId,
            Image = flower.Image,
            GenreList = genreSelectList
        };
        return View(flowerToEdit);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateFlower(FlowerDTO flowerToUpdate)
    {
        var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
        {
            Value = genre.Id.ToString(),
            Text = genre.GenreName,
            Selected = genre.Id == flowerToUpdate.GenreId // Set the selected genre   
        });
        flowerToUpdate.GenreList = genreSelectList;

        if (!ModelState.IsValid)
        {
            // If model state is not valid, return the view with the model
            return View(flowerToUpdate);
        }
        try
        {
            string oldImage = flowerToUpdate.Image; // Store the old image name for deletion later
            // Handle file upload if a file is provided
            if (flowerToUpdate.ImageFile != null)
            {
                string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];
                string imageName = await _fileService.SaveFile(flowerToUpdate.ImageFile, allowedExtensions);
                oldImage = flowerToUpdate.Image;
                flowerToUpdate.Image = imageName;
            }
            Flower flower = new()
            {
                Id = flowerToUpdate.Id,
                FlowerName = flowerToUpdate.FlowerName,
                LatinName = flowerToUpdate.LatinName,
                Description = flowerToUpdate.Description,
                Price = flowerToUpdate.Price,
                GenreId = flowerToUpdate.GenreId,
                Image = flowerToUpdate.Image
            };
            await _flowerRepo.UpdateFlower(flower);

            if (!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Flower was updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = "Sorry, something went wrong !!";
            return View(flowerToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = "File not found. Please upload a valid image file.";
            return View(flowerToUpdate);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "An unexpected error occurred. Please try again later.";
            return View(flowerToUpdate);
        }
    }

    public async Task<IActionResult> DeleteFlower(int id)
    {
        try
        {
            var flower = await _flowerRepo.GetFlowerById(id);
            if (flower == null)
            {
                throw new InvalidOperationException($"Flower with id: {id} not found");
            }
            else
            {
                await _flowerRepo.DeleteFlower(flower);
                if (!string.IsNullOrEmpty(flower.Image))
                {
                    _fileService.DeleteFile(flower.Image);
                }
            }
        }

        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = "Sorry, something went wrong !!";
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "An unexpected error occurred. Please try again later.";
        }
        return RedirectToAction(nameof(Index));
    }

}
