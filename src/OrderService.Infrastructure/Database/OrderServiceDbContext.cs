using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Database.Entities;

namespace OrderService.Infrastructure.Database;

internal sealed class OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
    : DbContext(options)
{
    public DbSet<OrderEntity> Orders => Set<OrderEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderServiceDbContext).Assembly);
    }
}
