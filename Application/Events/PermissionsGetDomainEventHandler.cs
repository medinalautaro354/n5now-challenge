using Application.Common.Services;
using Application.Dtos;
using Domain.DomainEvents;
using Domain.Enums;
using MediatR;

namespace Application.Events;

public class PermissionsGetDomainEventHandler : INotificationHandler<PermissionsGetDomainEvent>
{
    private readonly IPublisherService _publisher;

    public PermissionsGetDomainEventHandler(IPublisherService publisher)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task Handle(PermissionsGetDomainEvent notification, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid().ToString();

        await _publisher.Publish(new PublishDto(guid, OperationName.Get), cancellationToken);
    }
}