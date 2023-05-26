using Application.UseCases.V1.PermissionOperation.Commands.Create;
using FluentAssertions;

namespace Application.Test;

public class CreatePermissionValidationTest
{
    [Theory]
    [InlineData("", "pruenba", 1)]
    [InlineData("prueba", "", 1)]
    [InlineData("prueba", "dos", -1)]
    [InlineData("", "", 1)]
    [InlineData(null, null, 1)]
    public async Task Validation_WithPropertyIncorrect_IsValidFalse(string employeeForename, string employeeSurname, int permissionTypeId)
    {
        //Arrange
        var request = new CreatePermissionCommand(employeeForename, employeeSurname, permissionTypeId);

        var validator = new CreatePermissionValidation();

        //Act
        var result = await validator.ValidateAsync(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
    }
    
}