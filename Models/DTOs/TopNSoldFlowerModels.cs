namespace DreamGarden.Models.DTOs;


public record TopNSoldFlowerModel(string FlowerName, string LatinName, int TotalUnitSold);

public record TopNSoldFlowersVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldFlowerModel> TopNSoldFlowers);
