using Application.Common.Services;
using Application.Dtos;
using Domain.DomainEvents;
using Domain.Enums;
using MediatR;

namespace Application.Events;

public class PermissionUpdatedDomainEventHandler : INotificationHandler<PermissionUpdatedDomainEvent>
{
    private readonly IPublisherService _publisher;

    public PermissionUpdatedDomainEventHandler(IPublisherService publisher)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task Handle(PermissionUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid().ToString();

        await _publisher.Publish(new PublishDto(guid, OperationName.Modify), cancellationToken);
    }
}