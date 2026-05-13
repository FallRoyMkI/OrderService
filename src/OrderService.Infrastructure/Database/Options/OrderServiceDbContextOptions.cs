namespace OrderService.Infrastructure.Database.Options;

internal sealed class OrderServiceDbContextOptions
{
    public const string SectionKey = nameof(OrderServiceDbContextOptions);
    public string? ConnectionString { get; init; }
    public bool IsInMemory { get; init; }
}
