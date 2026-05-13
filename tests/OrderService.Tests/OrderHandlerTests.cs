using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Exceptions;
using OrderService.Application.Handlers.Order;
using OrderService.Application.Models;
using OrderService.Infrastructure.Database.Entities;
using OrderService.Infrastructure.Repositories.Orders;
using OrderService.Infrastructure.Services.ValidationService;

namespace OrderService.Tests;

public sealed class OrderHandlerTests
    : IAsyncLifetime
{
    private readonly DatabaseFixture _dbFixture;
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
    private readonly Mock<ILogger<OrderHandler>> _loggerHandlerMock = new();
    private readonly Mock<ILogger<OrderRepository>> _loggerRepositoryMock = new();

    public OrderHandlerTests(DatabaseFixture dbFixture)
    {
        _dbFixture = dbFixture;
    }

    [Fact]
    public async Task CreateOrder_Validation_Successful()
    {
        var orderHandler = new OrderHandler(_orderRepositoryMock.Object, new OrderValidationService(), _loggerHandlerMock.Object);
        var id = Guid.CreateVersion7();
        var order = new OrderModel
        {
            CityFrom = "Moscow",
            AdressFrom = "Moscow. Mainstreet 15.",
            CityTo = "Saint-Petersburg",
            AdressTo = "Saint-Petersburg. Mainstreet 15.",
            Weight = 50,
            PickupDate = DateOnly.FromDateTime(DateTime.Now)
        };
        _orderRepositoryMock.Setup(x => x.CreateOrder(order, It.IsAny<CancellationToken>()))
            .ReturnsAsync(id);

        var result = await orderHandler.CreateOrder(order, TestContext.Current.CancellationToken);

        Assert.Equal(id, result);
    }

    [Fact]
    public async Task CreateOrder_Validation_Unsuccessful()
    {
        var orderHandler = new OrderHandler(_orderRepositoryMock.Object, new OrderValidationService(), _loggerHandlerMock.Object);
        var id = Guid.CreateVersion7();
        var order = new OrderModel
        {
            CityFrom = "Moscow",
            AdressFrom = "",
            CityTo = "Saint-Petersburg",
            AdressTo = "Saint-Petersburg. Mainstreet 15.",
            Weight = 50,
            PickupDate = DateOnly.FromDateTime(DateTime.Now)
        };

        var action = () => { return orderHandler.CreateOrder(order, TestContext.Current.CancellationToken); };

        var exception = await Assert.ThrowsAsync<ValidationException>(action);

        Assert.Equal("Адресс отправки указан не корректно. Должен быть > 10 и <= 100", exception.Message);
    }

    [Fact]
    public async Task GetOrderById_GetOrder_Successful()
    {
        var id = Guid.CreateVersion7();
        var order = new OrderEntity
        {
            Id = id,
            CityFrom = "Moscow",
            AdressFrom = "Moscow. Mainstreet 15.",
            CityTo = "Saint-Petersburg",
            AdressTo = "Saint-Petersburg. Mainstreet 15.",
            Weight = 50,
            PickupDate = DateOnly.FromDateTime(DateTime.Now)
        };

        await _dbFixture.SetData(TestContext.Current.CancellationToken, order);

        var orderRepository = new OrderRepository(_dbFixture.DbContext, _loggerRepositoryMock.Object);
        var orderHandler = new OrderHandler(orderRepository, new OrderValidationService(), _loggerHandlerMock.Object);
        var orderModel = await orderHandler.GetOrderById(id, TestContext.Current.CancellationToken);

        Assert.Equal(order.Id, orderModel.Id);
        Assert.Equal(order.CityFrom, orderModel.CityFrom);
        Assert.Equal(order.CityTo, orderModel.CityTo);
        Assert.Equal(order.AdressFrom, orderModel.AdressFrom);
        Assert.Equal(order.AdressTo, orderModel.AdressTo);
        Assert.Equal(order.Weight, orderModel.Weight);
        Assert.Equal(order.PickupDate, orderModel.PickupDate);
    }

    [Fact]
    public async Task GetOrderById_GetOrder_Unsuccessful()
    {
        var id = Guid.CreateVersion7();

        var orderRepository = new OrderRepository(_dbFixture.DbContext, _loggerRepositoryMock.Object);
        var orderHandler = new OrderHandler(orderRepository, new OrderValidationService(), _loggerHandlerMock.Object);

        var action = () => { return orderHandler.GetOrderById(id, TestContext.Current.CancellationToken); };

        var exception = await Assert.ThrowsAsync<OrderNotFoundExcpetion>(action);

        Assert.Equal($"Нет заказа с ID = {id}", exception.Message);
    }

    [Fact]
    public async Task GetOrders_GetOrders_Successful()
    {
        var id = Guid.CreateVersion7();
        var order = new OrderEntity
        {
            Id = id,
            CityFrom = "Moscow",
            AdressFrom = "Moscow. Mainstreet 15.",
            CityTo = "Saint-Petersburg",
            AdressTo = "Saint-Petersburg. Mainstreet 15.",
            Weight = 50,
            PickupDate = DateOnly.FromDateTime(DateTime.Now)
        };

        var id2 = Guid.CreateVersion7();
        var order2 = new OrderEntity
        {
            Id = id2,
            CityFrom = "Moscow",
            AdressFrom = "Moscow. Mainstreet 15.",
            CityTo = "Saint-Petersburg",
            AdressTo = "Saint-Petersburg. Mainstreet 15.",
            Weight = 50,
            PickupDate = DateOnly.FromDateTime(DateTime.Now)
        };

        var entities = new List<OrderEntity>() { order, order2 };

        await _dbFixture.SetData(TestContext.Current.CancellationToken, entities);

        var orderRepository = new OrderRepository(_dbFixture.DbContext, _loggerRepositoryMock.Object);
        var orderHandler = new OrderHandler(orderRepository, new OrderValidationService(), _loggerHandlerMock.Object);
        var orders = await orderHandler.GetOrders(TestContext.Current.CancellationToken);

        Assert.Equal(orders.Count, entities.Count);
        Assert.Equal(orders[0].Id, entities[0].Id);
        Assert.Equal(orders[1].Id, entities[1].Id);
    }

    public async ValueTask DisposeAsync()
    {
        await _dbFixture.DisposeAsync();
    }

    public async ValueTask InitializeAsync()
    {
        await _dbFixture.InitializeAsync();
    }
}
