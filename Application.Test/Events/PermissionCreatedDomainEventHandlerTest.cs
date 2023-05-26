using Application.Common.Services;
using Application.Dtos;
using Application.Events;
using Domain.DomainEvents;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Test.Events;

public class PermissionCreatedDomainEventHandlerTest
{
    private readonly Mock<IPublisherService> _publisher;
    private PermissionCreatedDomainEventHandler _handler;

    public PermissionCreatedDomainEventHandlerTest()
    {
        _publisher = new Mock<IPublisherService>();

        _handler = new PermissionCreatedDomainEventHandler(_publisher.Object);
    }

    [Fact]
    public async Task Handle_ValidCreatedDomainEvent_CallsPublisherService()
    {
        // Arrange
        var domainEvent = new PermissionCreatedDomainEvent();

        // Act
        await _handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        _publisher.Verify(x => x.Publish(It.IsAny<PublishDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}