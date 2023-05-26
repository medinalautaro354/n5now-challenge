using System.Net;
using Application.UseCases.V1.PermissionOperation.Commands.Update;
using FluentAssertions;
using Moq;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;

namespace Application.Test.UseCases.PermissionOperation.Commands.Update;

public class UpdatePermissionCommandTest
{
    private readonly Mock<IPermissionRepository> _repository;
    private readonly Mock<IPermissionTypeRepository> _permissionTypeRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdatePermissionCommandHandler _handler;
    private CancellationToken _cancellationToken;

    public UpdatePermissionCommandTest()
    {
        //Arrange
        _repository = new Mock<IPermissionRepository>();
        _repository.Setup(x => x.GetById(1))
            .ReturnsAsync(
                new Permission(1, "Prueba", "Uno", new PermissionType(1, "User"), DateTime.UtcNow));
        _permissionTypeRepository = new Mock<IPermissionTypeRepository>();
        _permissionTypeRepository.Setup(x => x.GetById(1)).ReturnsAsync(new PermissionType(1, "User"));
        
        _unitOfWork = new Mock<IUnitOfWork>();
        _cancellationToken = CancellationToken.None;

        _handler = new UpdatePermissionCommandHandler(_repository.Object, _unitOfWork.Object, _permissionTypeRepository.Object);
    }

    [Fact]
    public async Task Handler_UpdatePermission_Success()
    {
        //Arrage
        var request = new UpdatePermissionCommand(1, "Update", "Prueba", 1);

        //Act
        var result = await _handler.Handle(request, _cancellationToken);

        //Assert
        result.IsValid.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Handler_UpdatePermission_PermissionNotExists_NotFound()
    {
        //Arrange
        var request = new UpdatePermissionCommand(2, "Update", "Prueba", 1);

        //Act
        var result = await _handler.Handle(request, _cancellationToken);
        
        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handler_UpdatePermission_PermissionTypeNotExists_NotFound()
    {
        //Arrange
        var request = new UpdatePermissionCommand(1, "Update", "Prueba", 2);

        //Act
        var result = await _handler.Handle(request, _cancellationToken);
        
        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsValid.Should().BeFalse();
    }
    
    
}