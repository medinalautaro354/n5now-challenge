using Application.UseCases.V1.PermissionOperation.Commands.Update;
using FluentAssertions;

namespace Application.Test.UseCases.PermissionOperation.Commands.Update;

public class UpdatePermissionValidationTest
{
    [Theory]
    [InlineData(1,"", "pruenba", 1)]
    [InlineData(0,"prueba", "", 1)]
    [InlineData(1,"prueba", "dos", -1)]
    [InlineData(1,"", "", 1)]
    [InlineData(-1,null, null, 1)]
    public async Task Validation_WithPropertyIncorrect_IsValidFalse(int permissionId, string employeeForename, string employeeSurname, int permissionTypeId)
    {
        //Arrange
        var request = new UpdatePermissionCommand(permissionId,employeeForename, employeeSurname, permissionTypeId);

        var validator = new UpdatePermissionValidation();

        //Act
        var result = await validator.ValidateAsync(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
    }
}