using System.Net;
using Application.UseCases.V1.PermissionOperation.Commands.Create;
using AutoFixture;
using Castle.Core.Logging;
using FluentAssertions;
using Moq;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;

namespace Application.Test;

public class CreatePermissionCommandTest
{
    private readonly Mock<IPermissionRepository> _repository;
    private readonly Mock<IPermissionTypeRepository> _permissionTypeRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreatePermissionCommandHandler _handler;
    private CancellationToken _cancellationToken;

    public CreatePermissionCommandTest()
    {
        //Arrange
        _repository = new Mock<IPermissionRepository>();
        _permissionTypeRepository = new Mock<IPermissionTypeRepository>();
        _permissionTypeRepository.Setup(x => x.GetById(1)).ReturnsAsync(new PermissionType(1, "User"));
        
        _unitOfWork = new Mock<IUnitOfWork>();
        _cancellationToken = CancellationToken.None;

        _handler = new CreatePermissionCommandHandler(_unitOfWork.Object, _repository.Object, _permissionTypeRepository.Object);
    }
    [Fact]
    public async Task Handle_CreatePermmission_Success()
    {
        //Arrange
        var request = new CreatePermissionCommand("Prueba", "Uno", 1);
        
        //Act
        var result = await _handler.Handle(request, _cancellationToken);
        
        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_CreatePermission_InvalidPermissionTypeId_BadRequest()
    {
        //Arrange
        var request = new CreatePermissionCommand("Prueba", "Dos", 5345);
        
        //Act
        var result = await _handler.Handle(request, _cancellationToken);
        
        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsValid.Should().BeFalse();
    }
}