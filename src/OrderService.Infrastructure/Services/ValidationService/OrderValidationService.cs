using OrderService.Application.Common.Interfaces;
using OrderService.Application.Models;

namespace OrderService.Infrastructure.Services.ValidationService;

internal sealed class OrderValidationService
    : IOrderValidationService
{
    public bool Validation(OrderModel orderModel, out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (!CityValidator(orderModel.CityFrom))
        {
            validationErrors.Add("Город отправки указан не корректно. Должен быть > 3 и <= 50");
        }

        if (!AdressValidator(orderModel.AdressFrom))
        {
            validationErrors.Add("Адресс отправки указан не корректно. Должен быть > 10 и <= 100");
        }

        if (!CityValidator(orderModel.CityTo))
        {
            validationErrors.Add("Город назначения указан не корректно. Должен быть > 3 и <= 50");
        }

        if (!AdressValidator(orderModel.AdressTo))
        {
            validationErrors.Add("Адресс назначения указан не корректно. Должен быть > 10 и <= 100");
        }

        if (!WeightValidator(orderModel.Weight))
        {
            validationErrors.Add("Вес указан не корректно. Должен быть > 0 и <= 50000");
        }

        if (!PickupDateValidator(orderModel.PickupDate))
        {
            validationErrors.Add("Дата забора груза указана не корректно. Должна быть не раньше текущего дня");
        }

        return validationErrors.Count == 0;
    }

    private static bool CityValidator(string city)
    {
        if (!string.IsNullOrWhiteSpace(city) && city.Length > 3 && city.Length <= 50)
        {
            return true;
        }

        return false;
    }

    private static bool AdressValidator(string adress)
    {
        if (!string.IsNullOrWhiteSpace(adress) && adress.Length > 10 && adress.Length <= 100)
        {
            return true;
        }

        return false;
    }

    private static bool WeightValidator(double weight)
    {
        if (weight > 0 && weight <= 50000)
        {
            return true;
        }

        return false;
    }

    private static bool PickupDateValidator(DateOnly dateOnly)
    {
        if (dateOnly >=  DateOnly.FromDateTime(DateTime.Now))
        {
            return true;
        }

        return false;
    }
}
