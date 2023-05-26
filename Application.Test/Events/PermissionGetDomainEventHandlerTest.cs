using Application.Common.Services;
using Application.Dtos;
using Application.Events;
using Domain.DomainEvents;
using Moq;

namespace Application.Test.Events;

public class PermissionGetDomainEventHandlerTest
{
    private readonly Mock<IPublisherService> _publisher;
    private PermissionsGetDomainEventHandler _handler;

    public PermissionGetDomainEventHandlerTest()
    {
        _publisher = new Mock<IPublisherService>();
        _handler = new PermissionsGetDomainEventHandler(_publisher.Object);
    }
    
    [Fact]
    public async Task Handle_ValidGetDomainEvent_CallsPublisherService()
    {
        // Arrange
        var domainEvent = new PermissionsGetDomainEvent();

        // Act
        await _handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        _publisher.Verify(x => x.Publish(It.IsAny<PublishDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}