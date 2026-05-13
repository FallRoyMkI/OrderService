using Microsoft.AspNetCore.Mvc;
using OrderService.ApiHost.Controllers.Common;
using OrderService.ApiHost.Mappers;
using OrderService.ApiHost.Requests.OrderRequests;
using OrderService.Application.Handlers.Order;

namespace OrderService.ApiHost.Controllers;

public sealed class OrderController(IOrderHandler handler)
    : BaseController
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest, CancellationToken cancellationToken = default)
    {
        var orderModel = createOrderRequest.Map();
        var result = await handler.CreateOrder(orderModel, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [Route("order/{orderId}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId, CancellationToken cancellationToken = default)
    {
        var result = await handler.GetOrderById(orderId, cancellationToken);
        return Ok(result.Map());
    }

    [HttpGet]
    [Route("orders")]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken = default)
    {
        var result = await handler.GetOrders(cancellationToken);
        return Ok(result.Map());
    }
}
