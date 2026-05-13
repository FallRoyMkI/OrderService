using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderService.Infrastructure.Database;

internal sealed class OrderServiceDbContextFactory
    : IDesignTimeDbContextFactory<OrderServiceDbContext>
{
    public OrderServiceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderServiceDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=postgres");

        return new OrderServiceDbContext(optionsBuilder.Options);
    }
}
