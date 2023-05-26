using System.Net;
using Application.Common.Services;
using Application.Dtos;
using Application.UseCases.V1.PermissionOperation.Queries.GetAll;
using FluentAssertions;
using Moq;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;

namespace Application.Test.UseCases.PermissionOperation.Queries.GetAll;

public class GetAllPermissionsTest
{
    private readonly Mock<IPermissionRepository> _repository;
    private GetAllPermissionsHandler _handler;
    private CancellationToken _cancellationToken;
    private readonly Mock<IPublisherService> _publisherService;

    public GetAllPermissionsTest()
    {
        _repository = new Mock<IPermissionRepository>();
        _publisherService = new Mock<IPublisherService>();
        _cancellationToken = CancellationToken.None;
        _handler = new GetAllPermissionsHandler(_repository.Object, _publisherService.Object);
    }

    [Fact]
    public async Task Handle_GetAllPermissions_Success()
    {
        //Arrange
        var request = new GetAllPermissions();
        var response = new List<PermissionDto>()
        {
            new PermissionDto(1, "Prueba","Uno", new PermissionTypeDto(1,"User")),
            new PermissionDto(2, "Prueba","Dos", new PermissionTypeDto(2,"Admin"))
        };
        var returnsRepository = new List<Permission>()
        {
            new Permission(1, "Prueba", "Uno", new PermissionType(1, "User"), DateTime.UtcNow),
            new Permission(2, "Prueba", "Dos", new PermissionType(2, "Admin"), DateTime.UtcNow)
        };
        _repository.Setup(f => f.GetAllAsync()).ReturnsAsync(returnsRepository);

        //Act
        var result = await _handler.Handle(request, _cancellationToken);
        
        
        //Assert
        result.Content.Should().Equal(response);
        result.IsValid.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}