using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Handlers.Order;

namespace OrderService.Application.Extensions;

public static class ServiceCollectionExtencion
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderHandler, OrderHandler>();

        return services;
    }
}
