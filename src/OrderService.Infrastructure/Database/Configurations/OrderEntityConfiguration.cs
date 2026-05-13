using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Infrastructure.Database.Entities;

namespace OrderService.Infrastructure.Database.Configurations;

internal sealed class OrderEntityConfiguration
    : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CityFrom).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AdressFrom).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CityTo).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AdressTo).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.PickupDate).IsRequired();
    }
}
