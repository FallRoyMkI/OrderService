using OrderService.ApiHost.Requests.OrderRequests;
using OrderService.ApiHost.Response.OrderResponse;
using OrderService.Application.Models;
using Riok.Mapperly.Abstractions;

namespace OrderService.ApiHost.Mappers;

[Mapper]
internal static partial class OrderMapper
{
    [MapperIgnoreTarget(nameof(OrderModel.Id))]
    public static partial OrderModel Map(this CreateOrderRequest createOrderRequest);
    public static partial OrderResponse Map(this OrderModel createOrderRequest);
    public static partial List<OrderResponse> Map(this List<OrderModel> createOrderRequests);
}
