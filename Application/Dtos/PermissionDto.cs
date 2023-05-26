
namespace Application.Dtos;
public record struct PermissionDto(int Id, string EmployeeForename, string EmployeeSurname, PermissionTypeDto PermissionType) { }
public record struct AddPermissionDto(string EmployeeForename, string EmployeeSurname, int PermissionTypeId) { }
public record struct UpdatePermissionDto(string EmployeeForename, string EmployeeSurname, int PermissionTypeId) { }


