using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Primitives;

namespace N5NowApi.Domain.Repositories;

public interface IPermissionTypeRepository : IRepository<PermissionType>, IPermissionTypeRepositoryReadOnly<PermissionType>
{
}

public interface IPermissionTypeRepositoryReadOnly<T> where T : Entity { }