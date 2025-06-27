using DreamGarden.Controllers;
using DreamGarden.Repositories;
using DreamGarden.Models.DTOs;
using Moq;
using NuGet.Protocol;

namespace test
{
    public class StockControllerTests
    {
        [Fact]
        public async Task Test_Index_Calls_GetStocks()
        {
            var mockRepo = new Mock<IStockRepository>();
            var controller = new StockController(mockRepo.Object);
            
            mockRepo.Setup(repo => repo.GetStocks(It.IsAny<string>()))
                .ReturnsAsync(new List<StockDisplayModel> { new StockDisplayModel { FlowerId = 1, Quantity = 10 } });
            
            await controller.Index("testTerm");

            Assert.Equal(1, mockRepo.Invocations.Count);
        }
    }
}