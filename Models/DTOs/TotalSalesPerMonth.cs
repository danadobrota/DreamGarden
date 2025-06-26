namespace DreamGarden.Models.DTOs;

 public record TotalSoldProducts(string FlowerName, string LatinName, int TotalUnitSold);

  public record TotalSoldProductsVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldFlowerModel> TopNSoldFlowers);

