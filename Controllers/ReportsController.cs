using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DreamGarden.Controllers;

[Authorize(Roles = nameof(Roles.Admin))] // Only users with the Admin role can access this controller   
public class ReportsController : Controller
{
    private readonly IReportRepository _reportRepository;

    //constructor injection for IReportRepository
    public ReportsController(IReportRepository reportRepository)
    {

        _reportRepository = reportRepository;
    }

    public async Task<ActionResult> TopFiveSellingFlowers(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
            //by default get the 7 days before the current date
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            //end date is today
            DateTime endDate = eDate ?? DateTime.UtcNow;

            //GetTopNSellingFlowersByDate is a method in the ReportRepository that calls the stored procedure to get the top 5 selling flowers
            var topSellingFlowers = await _reportRepository.GetTopNSellingFlowersByDate(startDate, endDate);
           
            var vm = new TopNSoldFlowersVm(startDate, endDate, topSellingFlowers);
            return View(vm);
        }

        catch (Exception ex)
        {
            TempData["errorMessage"] = "Sorry, something went wrong !!";
            //return to the Home Controller if this exception occurs
            return RedirectToAction("Index", "Home");
        }

    }


    public async Task<ActionResult> TotalSalesPerMonth(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
            //by default get the 7 days before the current date
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            //end date is today
            DateTime endDate = eDate ?? DateTime.UtcNow;


            var totalSales = await _reportRepository.GetTotalSalesPerMonth(startDate, endDate);

            var vm = new TotalSoldProductsVm(startDate, endDate, totalSales);
            return View(vm);
        }

        catch (Exception ex)
        {
            TempData["errorMessage"] = "Sorry, something went wrong !!";
            //return to the Home Controller if this exception occurs
            return RedirectToAction("Index", "Home");
        }

    }
}



