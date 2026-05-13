using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Database;
using OrderService.Infrastructure.Database.Entities;
using OrderService.Tests;
[assembly: AssemblyFixture(typeof(DatabaseFixture))]

namespace OrderService.Tests;

public sealed class DatabaseFixture
    : IAsyncLifetime
{
    internal OrderServiceDbContext DbContext { get; private set; }

    internal async Task SetData(CancellationToken cancellationToken, params IEnumerable<OrderEntity> orders)
    {
        await DbContext.Orders.AddRangeAsync(orders, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }

    public ValueTask InitializeAsync()
    {
        var name = Guid.CreateVersion7().ToString();
        var optionsBuilder = new DbContextOptionsBuilder<OrderServiceDbContext>();
        optionsBuilder.UseInMemoryDatabase(name);
        DbContext = new(optionsBuilder.Options);
        return ValueTask.CompletedTask;
    }
}
