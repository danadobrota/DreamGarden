namespace DreamGarden.Models.DTOs;

 public record TotalSoldProductModel(double Price);

 public record TotalSoldProductsVm(DateTime StartDate, DateTime EndDate, IEnumerable<TotalSoldProductModel> TotalSoldProducts);
 



