using Microsoft.Extensions.Logging;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Exceptions;
using OrderService.Application.Models;

namespace OrderService.Application.Handlers.Order;

public sealed class OrderHandler(IOrderRepository repository, IOrderValidationService validationService, ILogger<OrderHandler> logger)
    : IOrderHandler
{
    public async Task<Guid> CreateOrder(OrderModel order, CancellationToken cancellationToken)
    {
        if (!validationService.Validation(order, out var validationErrors))
        {
            var ex = new ValidationException(validationErrors);
            logger.LogError(ex, ex.Message);
            throw ex;
        }

        var result = await repository.CreateOrder(order, cancellationToken);
        return result;
    }

    public async Task<OrderModel> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await repository.GetOrderById(orderId, cancellationToken);
        return result;
    }

    public async Task<List<OrderModel>> GetOrders(CancellationToken cancellationToken)
    {
        var result = await repository.GetOrders(cancellationToken);
        return result;
    }
}
