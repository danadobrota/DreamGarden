namespace DreamGarden.Repositories
{

    public interface IReportRepository
    {
        Task<IEnumerable<TopNSoldFlowerModel>> GetTopNSellingFlowersByDate(DateTime startDate, DateTime endDate);

        Task<IEnumerable<TopNSoldFlowerModel>> GetTotalSalesPerMonth(DateTime startDate, DateTime endDate);    }



}
