using OrderService.Application.Models;

namespace OrderService.Application.Common.Interfaces;

public interface IOrderValidationService
{
    bool Validation(OrderModel orderModel, out List<string> validationErrors);
}
