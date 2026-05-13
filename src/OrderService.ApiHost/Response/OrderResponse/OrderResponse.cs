namespace OrderService.ApiHost.Response.OrderResponse;

public sealed class OrderResponse
{
    public Guid Id { get; set; }
    public string CityFrom { get; set; }
    public string AdressFrom { get; set; }
    public string CityTo { get; set; }
    public string AdressTo { get; set; }
    public double Weight { get; set; }
    public DateOnly PickupDate { get; set; }
}
