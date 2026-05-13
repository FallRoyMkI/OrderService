using OrderService.Application.Models;

namespace OrderService.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task<Guid> CreateOrder(OrderModel order, CancellationToken cancellationToken);
    Task<OrderModel> GetOrderById(Guid orderId, CancellationToken cancellationToken);
    Task<List<OrderModel>> GetOrders(CancellationToken cancellationToken);
}
