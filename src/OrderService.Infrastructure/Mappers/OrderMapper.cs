using OrderService.Application.Models;
using OrderService.Infrastructure.Database.Entities;
using Riok.Mapperly.Abstractions;

namespace OrderService.Infrastructure.Mappers;

[Mapper]
internal static partial class OrderMapper
{
    [MapperIgnoreSource(nameof(OrderModel.Id))]
    [MapperIgnoreTarget(nameof(OrderEntity.Id))]
    public static partial OrderEntity Map(this OrderModel orderModel);
    public static partial OrderModel Map(this OrderEntity orderEntity);
    public static partial List<OrderModel> Map(this List<OrderEntity> orderEntities);
}
