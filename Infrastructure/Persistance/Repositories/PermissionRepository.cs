using Microsoft.EntityFrameworkCore;
using N5NowApi.Domain.Entities;
using N5NowApi.Domain.Repositories;

namespace Infrastructure.Persistance.Repositories;
public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        var results = await _dbSet.Include(f => f.PermissionType).OrderBy(f => f.EmployeeForename)
            .ToListAsync();

        return results;
    }
}
