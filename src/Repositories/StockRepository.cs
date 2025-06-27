using Microsoft.EntityFrameworkCore;
namespace DreamGarden.Repositories

{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        //construtor to inject the ApplicationDbContext
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Method to get stock levels for a specific flower
        public async Task<Stock?> GetStockByFlowerId(int flowerId) => await _context.Stocks.FirstOrDefaultAsync(x => x.FlowerId == flowerId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            //check if the stock exists for the flower
            var existingStock = await GetStockByFlowerId(stockToManage.FlowerId);
            if (existingStock is null)
            {
                var stock = new Stock { FlowerId = stockToManage.FlowerId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock); //add new stock record
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity; //update the existing stock quantity

            }
            await _context.SaveChangesAsync(); //save changes to the database
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from flower in _context.Flowers
                                join stock in _context.Stocks on flower.Id equals stock.FlowerId
                                into flower_stock
                                from flowerStock in flower_stock.DefaultIfEmpty() //left join to include flowers with no stock
                                where string.IsNullOrWhiteSpace(sTerm) || flower.FlowerName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    FlowerId = flower.Id,
                                    FlowerName = flower.FlowerName,
                                    Quantity = flowerStock != null ? flowerStock.Quantity : 0, //if stock is null, set quantity to 0
                                }).ToListAsync();
            return stocks;
        }


    }

}
