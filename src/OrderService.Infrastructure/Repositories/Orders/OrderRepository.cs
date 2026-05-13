using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Exceptions;
using OrderService.Application.Models;
using OrderService.Infrastructure.Database;
using OrderService.Infrastructure.Mappers;

namespace OrderService.Infrastructure.Repositories.Orders;

internal sealed class OrderRepository(OrderServiceDbContext dbContext, ILogger<OrderRepository> logger)
    : IOrderRepository
{
    public async Task<Guid> CreateOrder(OrderModel order, CancellationToken cancellationToken)
    {
        try
        {
            var entity = order.Map();
            var entry = await dbContext.Orders.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return entry.Entity.Id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw ex;
        }
    }

    public async Task<OrderModel> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

            if (entity is null)
            {
                throw new OrderNotFoundExcpetion(orderId);
            }
            var order = entity.Map();

            return order;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw ex;
        }
    }

    public async Task<List<OrderModel>> GetOrders(CancellationToken cancellationToken)
    {
        try
        {
            var ordersEntities = await dbContext.Orders.ToListAsync(cancellationToken);

            return ordersEntities.Map();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw ex;
        }
    }
}
