namespace TestProject.Client.Models.Order;

public class ResponseCalculateOrderTotalModel
{
    public int OrderId { get; set; }
    public List<GuestOrderCalculation> GuestsOrdersCalculations { get; set; }
    public decimal OrderTotal { get; set; }
}

public class GuestOrderCalculation
{
    public int GuestId { get; set; }
    public decimal GuestOrderTotal { get; set; }
}