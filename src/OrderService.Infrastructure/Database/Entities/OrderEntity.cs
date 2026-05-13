using OrderService.Infrastructure.Database.Entities.Common;

namespace OrderService.Infrastructure.Database.Entities;

internal sealed class OrderEntity
    : BaseEntity
{
    public string CityFrom { get; set; }
    public string AdressFrom { get; set; }
    public string CityTo { get; set; }
    public string AdressTo { get; set; }
    public double Weight { get; set; }
    public DateOnly PickupDate { get; set; }
}
