using InventoryManagement.ProductCatalog.Api.Data;

using KJWT.SharedKernel.DomainEvents;

using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.ProductCatalog.Api.Infrastructure.Idempotence;

public sealed class IdempotentDomainEventHandler<TDomainEvent>
    : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> _decorated;
    private readonly ProductDbContext _dbContext;

    public IdempotentDomainEventHandler(
        INotificationHandler<TDomainEvent> decorated,
        ProductDbContext dbContext)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        if(await _dbContext.Set<Data.Outbox.OutboxMessageConsumer>()
                .AnyAsync(
                    outboxMessageConsumer =>
                        outboxMessageConsumer.Id == notification.Id &&
                        outboxMessageConsumer.Name == consumer,
                    cancellationToken))
        {
            return;
        }

        await _decorated.Handle(notification, cancellationToken);

        await _dbContext.Set<Data.Outbox.OutboxMessageConsumer>()
            .AddAsync(
            new Data.Outbox.OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            },
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}