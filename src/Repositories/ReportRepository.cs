using System.Data;
using DreamGarden.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DreamGarden.Repositories;
public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TopNSoldFlowerModel>> GetTopNSellingFlowersByDate(DateTime startDate, DateTime endDate)
    {
        var startDateParam = new SqlParameter("@startDate", startDate);
        var endDateParam = new SqlParameter("@endDate", endDate);
        //call method to get the result of the stored procedure from SQL which gives the top 5 selling flowers
        var topFiveSoldFlowers = await _context.Database.SqlQueryRaw<TopNSoldFlowerModel>("exec Usp_GetTopNSellingFlowersByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
        return topFiveSoldFlowers;
    }

    public async Task<IEnumerable<TotalSoldProductModel>> GetTotalSalesPerMonth(DateTime startDate, DateTime endDate)
    {
        var startDateParam = new SqlParameter("@startDate", startDate);
        var endDateParam = new SqlParameter("@endDate", endDate);
        //call method to get the result of the stored procedure from SQL which gives the total amount of sales per month
        var totalSalesPerMonth = await _context.Database.SqlQueryRaw<TotalSoldProductModel>("exec Usp_GetTotalSalesPerMonth @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
        return totalSalesPerMonth;
    }


}           



