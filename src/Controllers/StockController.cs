﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DreamGarden.Controllers
{
    [Authorize(Roles=nameof(Roles.Admin))] // Only users with the Admin role can access this controller
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo) // Constructor injection for IStockRepository
        {
            _stockRepo = stockRepo;
        }
        public async Task<IActionResult> Index(string sTerm="")
        {
            var stocks = await _stockRepo.GetStocks(sTerm);
            return View(stocks);
        }

        public async Task<IActionResult> ManageStock(int flowerId)
        {
            var existingStock = await _stockRepo.GetStockByFlowerId(flowerId);
            var stock = new StockDTO
            {
                FlowerId = flowerId,
                Quantity = existingStock != null ? existingStock.Quantity : 0
            };
            return View(stock);
        }

        [HttpPost]

        public async Task<IActionResult> ManageStock(StockDTO stock)
        {
            if (!ModelState.IsValid)
                return View(stock);
            try
            {
                await _stockRepo.ManageStock(stock);
                TempData["successMessage"] = "Stock was updated successfully";
            }

            catch(Exception ex)
            {
                TempData["errorMessage"] = "Sorry, something went wrong !!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
