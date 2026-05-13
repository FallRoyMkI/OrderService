namespace OrderService.Application.Exceptions;

public sealed class OrderNotFoundExcpetion(Guid orderId)
    : Exception($"Нет заказа с ID = {orderId}");
