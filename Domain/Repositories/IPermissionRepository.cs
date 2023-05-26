using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Primitives;
using System.Threading.Tasks;

namespace N5NowApi.Domain.Repositories;
public interface IPermissionRepository : IRepository<Permission>
{
    Task<IEnumerable<Permission>> GetAllAsync();
}
