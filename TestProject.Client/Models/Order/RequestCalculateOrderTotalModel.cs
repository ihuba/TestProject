namespace TestProject.Client.Models.Order;

public class RequestCalculateOrderTotalModel
{
    public List<GuestsOrderModel> GuestsOrderModels { get; set; }
}

public class GuestsOrderModel
{
    public int? GuestId { get; set; }
    public int DrinksWithDiscountCount { get; set; }
    public int DrinksWithoutDiscountCount { get; set; }
    public int StartersCount { get; set; }
    public int MainsCount { get; set; }
}

