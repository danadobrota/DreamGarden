namespace DreamGarden.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByFlowerId(int flowerId);
        Task ManageStock(StockDTO stockToManage);
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
    }

}
