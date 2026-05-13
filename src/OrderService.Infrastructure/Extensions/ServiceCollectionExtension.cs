using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.Common.Interfaces;
using OrderService.Infrastructure.Database;
using OrderService.Infrastructure.Database.Options;
using OrderService.Infrastructure.Repositories.Orders;
using OrderService.Infrastructure.Services.ValidationService;

namespace OrderService.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    private static readonly string _dbName = Guid.CreateVersion7().ToString();
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<OrderServiceDbContextOptions>()
            .Bind(configuration.GetSection(OrderServiceDbContextOptions.SectionKey));

        services.AddDbContext<OrderServiceDbContext>((sp, optionsBuilder) =>
        {
            var options = sp.GetRequiredService<IOptions<OrderServiceDbContextOptions>>();
            if (options.Value.IsInMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_dbName);
            }
            else
            {
                ArgumentNullException.ThrowIfNullOrEmpty(options.Value.ConnectionString);
                optionsBuilder.UseNpgsql(options.Value.ConnectionString);
            }
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderValidationService, OrderValidationService>();

        return services;
    }
}
