using OrderService.Application.Models;

namespace OrderService.Application.Handlers.Order;

public interface IOrderHandler
{
    Task<Guid> CreateOrder(OrderModel order, CancellationToken cancellationToken);
    Task<OrderModel> GetOrderById(Guid orderId, CancellationToken cancellationToken);
    Task<List<OrderModel>> GetOrders(CancellationToken cancellationToken);
}
