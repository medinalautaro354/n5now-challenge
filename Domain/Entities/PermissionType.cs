using N5NowApi.Domain.Dtos;
using N5NowApi.Domain.Primitives;
using N5NowApi.Domain.ValueObjects.PermissionType;

namespace N5NowApi.Domain.Entities;
public class PermissionType : Entity
{
    public PermissionType(int id, string description) : base(id)
    {
        Description = Description.Create(description);
    }

    public Description Description { get; private set; }
    public IEnumerable<Permission> Permission { get; private set; }
    private PermissionType(int id) : base(id) { }
    public static PermissionType Create(AddPermissionTypeDto addDto)
    {
        var permissionType = new PermissionType(0) { Description = addDto.Description };

        return permissionType;
    }
}