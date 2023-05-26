using Domain.DomainEvents;
using N5NowApi.Domain.Dtos;
using N5NowApi.Domain.Primitives;
using N5NowApi.Domain.ValueObjects.Permission;

namespace N5NowApi.Domain.Entities;

public class Permission : AggregateRoot
{
    
    public Permission(int id, string employeeForename, string employeeSurname, PermissionType permissionType, DateTime permissionDate) : base(id)
    {
        EmployeeForename = EmployeeForename.Create(employeeForename);
        EmployeeSurname = EmployeeSurname.Create(employeeSurname);
        PermissionType = permissionType;
        PermissionDate = permissionDate;
    }

    public EmployeeForename EmployeeForename { get; private set; }
    public EmployeeSurname EmployeeSurname { get; private set; }
    public int PermissionTypeId { get; private set; }
    public PermissionType PermissionType { get; private set; }
    public DateTime PermissionDate { get; private set; }

    private Permission(int id) : base(id)
    {
    }

    public static Permission Create(AddPermissionDto addDto)
    {
        var permission = new Permission(0)
        {
            PermissionTypeId = addDto.PermissionTypeId,
            EmployeeForename = addDto.EmployeeForename,
            EmployeeSurname = addDto.EmployeeSurname,
            PermissionDate = DateTime.UtcNow,
        };

        return permission;
    }

    public static Permission UpdateProperties(Permission entity, UpdatePermissionDto updatePermissionDto)
    {
        if(entity.EmployeeForename != updatePermissionDto.EmployeeForename)
        {
            entity.EmployeeForename = updatePermissionDto.EmployeeForename;
        }

        if(entity.EmployeeSurname != updatePermissionDto.EmployeeSurname)
        {
            entity.EmployeeSurname = updatePermissionDto.EmployeeSurname;
        }

        if(entity.PermissionTypeId != updatePermissionDto.PermissionTypeId)
        {
            entity.PermissionTypeId= updatePermissionDto.PermissionTypeId;
        }

        return entity;
    }

}

