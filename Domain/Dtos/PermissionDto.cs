using N5NowApi.Domain.Entities;
using N5NowApi.Domain.ValueObjects.Permission;

namespace N5NowApi.Domain.Dtos;
public record struct AddPermissionDto(EmployeeForename EmployeeForename, EmployeeSurname EmployeeSurname, int PermissionTypeId)
{
}

public record struct UpdatePermissionDto(int Id, EmployeeForename EmployeeForename, EmployeeSurname EmployeeSurname, int PermissionTypeId) { }
