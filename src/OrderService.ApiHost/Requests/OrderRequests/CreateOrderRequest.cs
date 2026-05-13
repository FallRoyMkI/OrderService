namespace OrderService.ApiHost.Requests.OrderRequests;

public sealed class CreateOrderRequest
{
    public string CityFrom { get; set; }
    public string AdressFrom { get; set; }
    public string CityTo { get; set; }
    public string AdressTo { get; set; }
    public double Weight { get; set; }
    public DateOnly PickupDate { get; set; }
}
