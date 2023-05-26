using Application.Common.Services;
using Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Events;
public class PermissionCreatedDomainEventHandler : INotificationHandler<PermissionCreatedDomainEvent>
{
    private readonly IPublisherService _publisherService;

    public PermissionCreatedDomainEventHandler(IPublisherService publisherService)
    {
        _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
    }

    public async Task Handle(PermissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid().ToString();

        await _publisherService.Publish(
            new Dtos.PublishDto(guid, Domain.Enums.OperationName.Request),
            cancellationToken);
    }
}