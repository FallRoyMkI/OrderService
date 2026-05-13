namespace OrderService.Infrastructure.Database.Entities.Common;

internal abstract class BaseEntity
    : BaseEntity<Guid>;

internal abstract class BaseEntity<TId>
    where TId : notnull
{
    public TId Id { get; set; }
}
