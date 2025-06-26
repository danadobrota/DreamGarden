using System.Data;
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

    public async Task<IEnumerable<TopNSoldFlowerModel>> GetTotalSalesPerMonth(DateTime startDate, DateTime endDate)
    {
        var startDateParam = new SqlParameter("@startDate", startDate);
        var endDateParam = new SqlParameter("@endDate", endDate);
        //call method to get the result of the stored procedure from SQL which gives the top 5 selling flowers
        var totalSalesPerMonth = await _context.Database.SqlQueryRaw<TopNSoldFlowerModel>("exec Usp_GetTopNSellingFlowersByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
        return totalSalesPerMonth;
    }


}           



